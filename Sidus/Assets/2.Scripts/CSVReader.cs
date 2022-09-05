using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    // CVS������ ���� ������ ����ǥ�����̴�.
    private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    // CVS������ ���� ��ä���� ������ ����ǥ�����̴�.
    private static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    // \"�� �������ش�.
    private static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        // ��ȯ�� ����Ʈ ������ �����Ѵ�.
        var list = new List<Dictionary<string, object>>();
        file = "CSVFolder/" + file;
        // CVS������ �����´�.
        TextAsset data = Resources.Load(file) as TextAsset;

        // data�� �ٷ� ������ ������ �����Ѵ�.
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // ���� 1�� ���϶�� �״�� ��ȯ�Ѵ�.
        // �ֳ��ϸ� ù��°���� ����̱� ������ 1�������� ��� �����Ͱ� ���� �Ͱ� ����.
        if (lines.Length <= 1) return list;

        // 2�� �̻��� ��� ù��° ���� ����� ������ �����Ѵ�.
        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            // ���� ������ �����Ͱ� ���� ��� ��ŵ
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                // \"�� �� �ڷ� ��� �������ְ� \�� �������� ������ �ش�.
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;

                // ���� value�� int �Ǵ� float���� ��ȯ�� �����ϴٸ� ��ȯ�Ͽ� �����Ѵ�.
                if (int.TryParse(value, out int n))
                    finalvalue = n;
                else if (float.TryParse(value, out float f))
                    finalvalue = f;

                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}