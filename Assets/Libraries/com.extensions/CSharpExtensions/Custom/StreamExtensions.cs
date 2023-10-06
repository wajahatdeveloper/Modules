using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


public static class StreamExts
    {
        /// <summary>
        /// 输出数组；
        /// </summary>
        /// <param name="this">流</param>
        /// <returns>数组内容</returns>
        public static byte[] ToArray(this Stream @this)
        {
            @this.Position = 0;
            byte[] bytes = new byte[@this.Length];
            @this.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            @this.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        ///  读取所有内容行；
        /// </summary>
        /// <param name="this">流读取器</param>
        /// <param name="closeAfter">使用后关闭</param>
        /// <returns>行内容</returns>
        public static List<string> ReadAllLines(this StreamReader @this, bool closeAfter = true)
        {
            var stringList = new List<string>();
            string str;
            while ((str = @this.ReadLine()) != null)
            {
                stringList.Add(str);
            }
            if (closeAfter)
            {
                @this.Close();
                @this.Dispose();
            }
            return stringList;
        }
        /// <summary>
        /// 读取所有内容行；
        /// </summary>
        /// <param name="this">文件流</param>
        /// <param name="encoding">编码</param>
        /// <param name="closeAfter">使用后关闭</param>
        /// <returns>行内容</returns>
        public static List<string> ReadAllLines(this FileStream @this, Encoding encoding, bool closeAfter = true)
        {
            var stringList = new List<string>();
            string str;
            var sr = new StreamReader(@this, encoding);
            while ((str = sr.ReadLine()) != null)
            {
                stringList.Add(str);
            }
            if (closeAfter)
            {
                sr.Close();
                sr.Dispose();
                @this.Close();
                @this.Dispose();
            }

            return stringList;
        }
        /// <summary>
        /// 读取所有文本；
        /// </summary>
        /// <param name="this">文件流</param>
        /// <param name="encoding">编码</param>
        /// <param name="closeAfter">使用后关闭</param>
        /// <returns>文本内容</returns>
        public static string ReadAllText(this FileStream @this, Encoding encoding, bool closeAfter = true)
        {
            var sr = new StreamReader(@this, encoding);
            var text = sr.ReadToEnd();
            if (closeAfter)
            {
                sr.Close();
                sr.Dispose();
                @this.Close();
                @this.Dispose();
            }

            return text;
        }
        /// <summary>
        /// 写入所有文本;
        /// </summary>
        /// <param name="this">文件流</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码</param>
        /// <param name="closeAfter">使用后关闭</param>
        public static void WriteAllText(this FileStream @this, string content, Encoding encoding, bool closeAfter = true)
        {
            var sw = new StreamWriter(@this, encoding);
            @this.SetLength(0);
            sw.Write(content);
            if (closeAfter)
            {
                sw.Close();
                sw.Dispose();
                @this.Close();
                @this.Dispose();
            }
        }


        /// <summary>
        /// 以文件流的形式复制大文件
        /// </summary>
        /// <param name="this">源</param>
        /// <param name="dest">目标地址</param>
        /// <param name="bufferSize">缓冲区大小，默认8MB</param>
        public static void CopyToFile(this Stream @this, string dest, int bufferSize = 1024 * 8 * 1024)
        {
            using (var fsWrite = new FileStream(dest, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buf = new byte[bufferSize];
                int len;
                while ((len = @this.Read(buf, 0, buf.Length)) != 0)
                {
                    fsWrite.Write(buf, 0, len);
                }
            }
        }
        /// <summary>
        /// 以文件流的形式复制大文件(异步方式)
        /// </summary>
        /// <param name="this">源</param>
        /// <param name="dest">目标地址</param>
        /// <param name="bufferSize">缓冲区大小，默认8MB</param>
        public static async Task CopyToFileAsync(this Stream @this, string dest, int bufferSize = 1024 * 1024 * 8)
        {
            using (var fsWrite = new FileStream(dest, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buf = new byte[bufferSize];
                int len;
                await Task.Run(() =>
                {
                    while ((len = @this.Read(buf, 0, buf.Length)) != 0)
                    {
                        fsWrite.Write(buf, 0, len);
                    }
                }).ConfigureAwait(true);
            }
        }
        /// <summary>
        /// 将内存流转储成文件
        /// </summary>
        /// <param name="this"></param>
        /// <param name="filename"></param>
        public static void SaveFile(this MemoryStream @this, string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = @this.ToArray(); // 转化为byte格式存储
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }
        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="this">源文件流</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileMD5(this FileStream @this)
        {
            return HashFile(@this, "md5");
        }
        /// <summary>
        /// 计算文件的 sha1 值
        /// </summary>
        /// <param name="this">源文件流</param>
        /// <returns>sha1 值16进制字符串</returns>
        public static string GetFileSha1(this Stream @this)
        {
            return HashFile(@this, "sha1");
        }
        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        /// <param name="fs">被操作的源数据流</param>
        /// <param name="algo">加密算法</param>
        /// <returns>哈希值16进制字符串</returns>
        static string HashFile(Stream fs, string algo)
        {
            HashAlgorithm crypto = default;
            switch (algo)
            {
                case "sha1":
                    crypto = new SHA1CryptoServiceProvider();
                    break;
                default:
                    crypto = new MD5CryptoServiceProvider();
                    break;
            }
            byte[] retVal = crypto.ComputeHash(fs);
            StringBuilder sb = new StringBuilder();
            foreach (var t in retVal)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

    }