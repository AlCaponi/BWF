<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="_User" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Kandidat</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container">
			    <h1><%=Server.HtmlEncode(S.Vorname)%> <%=Server.HtmlEncode(S.Nachname)%></h1>
			    <table>
			        <tr>
			            <th>Frage</th>
			            <th>Antwort</th>
			        </tr>
                    <%if (!S.Beantwortet)
                      {%>
                    <tr>
                        <td colspan="2"><i>Soldat hat noch keine Antworten</i></td>
                    </tr>
                    <%}
                      else
                      {%>
                    <tr>
                        <td>Problematischer Soldat</td>
                        <td class="<%=S.Problematic?"fail":"OK" %>"><%=S.Problematic ? "Ja" : "Nein"%></td>
                    </tr>
                    <%foreach (Base.Poll P in S.Antworten)
                      {%>
			        <tr>
			            <td><%=Server.HtmlEncode(P.Frage)%></td>
			            <td><%=string.IsNullOrEmpty(P.Antwort) ? "<i class=\"fail\">Keine Antwort</i>" : Server.HtmlEncode(P.Antwort).Replace("\r\n", "<br />")%></td>
			        </tr>
                    <%}%>
                    <%}%>
			    </table>
			    <br />
			    <form method="get" action="Details.aspx">
                    <input type="hidden" name="ID" value="<%=Server.HtmlEncode(S.Anlass.ToString())%>" />
			        <input type="submit" class="btn btn-default" value="&lt;&lt; Zurück" />
			    </form>
			</div>
		</div>
	</body>
</html>
