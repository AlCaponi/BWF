<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="_New" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Neue Umfrage</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="/js/moment.js"></script>
        <script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
        <link rel="stylesheet" type="text/css" href="/css/bootstrap-datetimepicker.min.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container login-form">
				<form method="post" action="New2.aspx" enctype="multipart/form-data">
					<div class="form-group">
						<label for="anlass">Anlass</label>
						<input type="text" class="form-control" id="anlass" name="anlass" placeholder="Anlass" />
					</div>
					<div class="form-group">
						<label for="datum">Datum</label>
                        <div class="input-group" id="datepicker">
						    <input type="text" class="form-control" id="datum" name="datum" placeholder="Datum (DD.MM.YYYY HH:MM)" />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
					</div>
					<div class="form-group">
						<label for="excel">Teilnehmerliste</label>
						<input type="file" class="form-control" id="excel" name="excel" accept=".xls,application/vnd.ms-excel" />
					</div>
                    <div class="form-group">
					    <input type="submit" value="Weiter &gt;&gt;" class="btn btn-primary" />
                    </div>
                    <div class="form-group">
					    <input type="button" id="cancel" value="Abbrechen" class="btn btn-default" />
                    </div>
                    <br />
                    <div class="alert alert-warning" role="alert">
                        Ist eine SV Nummer bereits einem anderen Anlass zugeordnet, wird der Soldat hier zugeordnet und die bestehenden Antworten beibehalten.
                    </div>

                    <%if (!string.IsNullOrEmpty(err)){ %>
                    <div class="alert alert-danger" role="alert"><%=Server.HtmlEncode(err)%></div>
                    <%}%>
				</form>
			</div>
		</div>
    <script type="text/javascript">
        $(function () {
            $("#cancel").on("click", function () { document.location.href = "/Admin/"; return false; });
            $('#datepicker').datetimepicker(
                {
                    locale:"de",
                    minDate:new Date()
                });
        });
        </script>
	</body>
</html>
