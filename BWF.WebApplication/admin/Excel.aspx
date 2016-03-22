<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel.aspx.cs" Inherits="Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Excel Test</title>
		<script type="text/javascript" src="js/jquery.js"></script>
		<script type="text/javascript" src="js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container">
                <form method="post" enctype="multipart/form-data">
                    <input type="file" name="EX" class="form-control" accept=".xls,.xlsx"/><br />
                    <input type="submit" value="Read" class="btn btn-success" />
                </form>
                <%=Status %>
            </div>
        </div>
    </body>
</html>
