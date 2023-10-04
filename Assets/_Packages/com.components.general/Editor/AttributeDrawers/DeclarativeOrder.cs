/* Copyright Kupio Limited. Registered in Scotland; SC426881.
 * All rights reserved. Not for distribution. */

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEngine;

    public class DeclarativeOrder
    {
        struct Edge
        {
            public Type from;
            public Type to;
        }

        private static HashSet<Type> nodes = new HashSet<Type>();
        private static HashSet<Edge> edges = new HashSet<Edge>();
        private static Dictionary<Type, int> depCount = new Dictionary<Type, int>();


        private static void AddNode(Type t)
        {
            nodes.Add(t);
            if (depCount.ContainsKey(t) == false)
            {
                depCount[t] = 0;
            }
        }

        [DidReloadScripts]
        public static void OnScriptsLoaded()
        {
            nodes.Clear();
            edges.Clear();
            depCount.Clear();

            Type runFirst = null;
            Type runLast = null;

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type typ in types)
                {
                    bool hasOrdering = false;

                    if (typ.IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        object[] attribs = typ.GetCustomAttributes(true);
                        for (int i = 0; i < attribs.Length; i++)
                        {
                            var atype = attribs[i].GetType();
                            if (atype == typeof(RunAfter))
                            {
                                hasOrdering = true;

                                AddNode(typ);

                                RunAfter deps = attribs[i] as RunAfter;

                                foreach (Type depType in deps.All)
                                {
                                    AddNode(depType);
                                    edges.Add(new Edge() { from = depType, to = typ });
                                    depCount[typ] = depCount[typ] + 1;
                                }
                            }
                            else if (atype == typeof(RunBefore))
                            {
                                hasOrdering = true;

                                AddNode(typ);

                                RunBefore deps = attribs[i] as RunBefore;

                                foreach (Type depType in deps.All)
                                {
                                    AddNode(depType);
                                    edges.Add(new Edge() { from = typ, to = depType });
                                    depCount[depType] = depCount[depType] + 1;
                                }
                            }
                        }

                        for (int i = 0; i < attribs.Length; i++)
                        {
                            var atype = attribs[i].GetType();

                            if (atype == typeof(RunFirst))
                            {
                                if (hasOrdering)
                                {
                                    Debug.LogError("RunFirst must be used alone. It can't also be used with RunAfter, or RunBefore: " + typ.ToString());
                                    return;
                                }
                                if (runFirst != null)
                                {
                                    Debug.LogError("Two classes marked as 'RunFirst'. You can only pick one: " + runFirst.ToString() + " <=> " + typ.ToString());
                                    return;
                                }
                                runFirst = typ;
                            }
                            else if (atype == typeof(RunLast))
                            {
                                if (hasOrdering)
                                {
                                    Debug.LogError("RunLast must be used alone. It can't also be used with RunAfter, or RunBefore: " + typ.ToString());
                                    return;
                                }
                                if (runLast != null)
                                {
                                    Debug.LogError("Two classes marked as 'RunLast'. You can only pick one: " + runLast.ToString() + " <=> " + typ.ToString());
                                    return;
                                }
                                runLast = typ;
                            }
                        }

                    }
                }
            }

            if (runFirst != null && runFirst == runLast)
            {
                Debug.LogError("A class cannot be marked as RunFirst and RunLast at the same time: " + runLast.ToString());
                return;
            }

            List<Type> ordered = new List<Type>();
            Queue<Type> start = new Queue<Type>();
            foreach (Type typ in depCount.Keys)
            {
                if (depCount[typ] == 0)
                {
                    start.Enqueue(typ);
                }
            }

            while (start.Count > 0)
            {
                Type next = start.Dequeue();
                ordered.Remove(next);
                ordered.Add(next);
                var enumerator = edges.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Edge edge = enumerator.Current;

                    if (edge.from == next)
                    {
                        edges.Remove(edge);
                        enumerator = edges.GetEnumerator();

                        int count = 0;
                        foreach (Edge e in edges)
                        {
                            if (e.to == edge.from)
                            {
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            start.Enqueue(edge.to);
                        }
                    }
                }
            }

            int step = 10;

            Dictionary<Type, int> order = new Dictionary<Type, int>();
            for (int i = 0; i < ordered.Count; i++)
            {
                order[ordered[i]] = (i + (runFirst==null?1:2)) * step;
            }

            if (runLast != null)
            {
                order[runLast] = (ordered.Count + (runFirst == null ? 1 : 2)) * step;
            }

            if (runFirst != null)
            {
                order[runFirst] = step;
            }

            if (edges.Count > 0)
            {
                foreach (var ed in edges)
                {
                    Debug.LogError("Cannot update script order due to circular dependency: " + ed.from.ToString() + " <=> " + ed.to.ToString());
                }
                return;
            }
            else
            {
                MonoScript[] scripts = MonoImporter.GetAllRuntimeMonoScripts();

                for (int i = 0; i < scripts.Length; i++)
                {
                    Type t = scripts[i].GetClass();

                    if (t != null && order.ContainsKey(t))
                    {
                        if (MonoImporter.GetExecutionOrder(scripts[i]) != order[t])
                        {
                            MonoImporter.SetExecutionOrder(scripts[i], order[t]);
                        }
                        order.Remove(t);
                    }
                }

                if (order.Count > 0)
                {
                    foreach (var t in order)
                    {
                        Debug.LogWarning("Unable to set execution order of " + t.Key.FullName + ". The MonoBehaviour class name must match its filename.");
                    }
                }
            }
        }
    }