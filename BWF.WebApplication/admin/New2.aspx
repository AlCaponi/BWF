<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New2.aspx.cs" Inherits="_New2" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Neue Umfrage</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container login-form">
                <h1>Neue Umfrage</h1>
                <%if (FieldCount > 3)
                  { %>
                Bitte geben Sie die entsprechenden Felder aus der Tabelle an
				<form method="post" action="New3.aspx" enctype="multipart/form-data">
                    <input type="hidden" name="ID" value="<%=ExcelFile %>" />
                    <input type="hidden" name="anlassname" value="<%=Anlass %>" />
                    <input type="hidden" name="anlassdatum" value="<%=Datum %>" />
					<div class="form-group">
						<label for="svnr">SV Nummer (AHV Nummer)</label><br />
						<select class="form-control" name="svnr" id="svnr">
                            <%=ToSelect(Tables.ToArray())%>
                        </select>
					</div>
					<div class="form-group">
						<label for="vorname">Vorname</label><br />
						<select class="form-control" name="vorname" id="vorname">
                            <%=ToSelect(Tables.ToArray())%>
                        </select>
					</div>
					<div class="form-group">
						<label for="nachname">Nachname</label><br />
						<select class="form-control" name="nachname" id="nachname">
                            <%=ToSelect(Tables.ToArray())%>
                        </select>
					</div>
					<div class="form-group">
						<label for="datum">Geburtsdatum</label><br />
						<select class="form-control" name="datum" id="datum">
                            <%=ToSelect(Tables.ToArray())%>
                        </select>
					</div>
                    <div class="form-group">
					    <input type="submit" value="Umfrage Erstellen" class="btn btn-primary" />
                    </div>
                    <div class="form-group">
					    <input type="button" id="cancel" value="Abbrechen" class="btn btn-default" />
                    </div>
                    <br />
                    <%if (!string.IsNullOrEmpty(err))
                      { %>
                    <div class="alert alert-danger" role="alert"><%=Server.HtmlEncode(err)%></div>
                    <%}%>
				</form>
                <%}
                  else
                  {%>
                    <h2>Ungültige Excel Datei</h2>
                    Die Datei muss mindestens 4 Spalten aufweisen: SV-Nummer, Vorname, Nachname, Geburtsdatum
                  <%}%>
			</div>
		</div>
        <script type="text/javascript">
            $(document).ready(
            function () {
                $("#cancel").on("click", function () { document.location.href = "/Admin/"; return false; });
            }
            );
        </script>
	</body>
</html>
