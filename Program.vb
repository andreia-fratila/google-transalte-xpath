Imports System.Net.Http
Imports HtmlAgilityPack

Module GoogleTranslate
    Sub Main()
        'Uipath input arguments

        Dim urlTemplate As String = "https://translate.google.com/?sl={0}&tl={1}&text={2}&op=translate"
        Dim sourceLanguage As String = "bg"
        Dim translatedLanguage As String = "en"
        Dim textToBeTranslated As String = parseText("ДОСТАВКА НА МЕДИЦИНСКИ ИЗДЕЛИЯ, КОИТО НЕ СЕ ЗАПЛАЩАТ ОТ НЗОК ЗА НУЖДИТЕ НА СПЕЦИАЛИСТИ-ОЧНИ БОЛЕСТИ")

        Dim url As Uri = New Uri(String.Format(urlTemplate, sourceLanguage, translatedLanguage, textToBeTranslated))


        Dim httpClient As New HttpClient
        httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime")


        Dim htmlPage As String = httpClient.GetStringAsync(url.ToString).Result
        Dim htmlDoc As New HtmlDocument
        htmlDoc.LoadHtml(htmlPage)
        Dim fullXpath As String = "/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[2]/div/div[8]/div/div[1]/span[1]/span/span"
        Dim translation As IEnumerable(Of HtmlNode) = htmlDoc.DocumentNode.SelectNodes(fullXpath)
        Console.WriteLine(translation.Count)



    End Sub

    Function parseText(ByVal sourceText As String) As String

        Dim specialCharacters As String()() = {
            ({"%", "%25"}),
            ({" ", "%20"}),
            ({",", "%2C"}),
            ({"?", "%3F"}),
            ({"\n", "%0A"}),
            ({"""", "%22"}),
            ({"<", "%3C"}),
            ({">", "%3E"}),
            ({"#", "%23"}),
            ({"|", "%7C"}),
            ({"&", "%26"}),
            ({"=", "%3D"}),
            ({"@", "%40"}),
            ({"#", "%23"}),
            ({"$", "%24"}),
            ({"^", "%5E"}),
            ({"`", "%60"}),
            ({"+", "%2B"}),
            ({"\'", "%27"}),
            ({"{", "%7B"}),
            ({"}", "%7D"}),
            ({"[", "%5B"}),
            ({"]", "%5D"}),
            ({"/", "%2F"}),
            ({"\\", "%5C"}),
            ({":", "%3A"}),
            ({";", "%3B"})
            }
        For Each characterPair As String() In specialCharacters
            sourceText = sourceText.Replace(characterPair(0), characterPair(1))
        Next
        Return sourceText
    End Function
End Module
