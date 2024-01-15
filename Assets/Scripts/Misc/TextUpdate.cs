using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataLayer;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        ParseText();
    }

    [ContextMenu(nameof(ParseText))]
    private void ParseText()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        string txt = _textMeshProUGUI.text;
        var output = Regex.Matches(txt, @"\<(.+?)\>")
            .Cast<Match>()
            .Select(m => m.Groups[1].Value).ToList();
        ApplySubstitutions(output);
    }

    private void ApplySubstitutions(List<string> output)
    {
        foreach (string s in output)
        {
            ReplaceValue(s);
        }
    }

    private void ReplaceValue(string s)
    {
        switch (s)
        {
            case "<localkillcount>":
                _textMeshProUGUI.text = _textMeshProUGUI.text.Replace(s,LocalData.Instance.GetKillCount().ToString());
                break;
        }
    }
}