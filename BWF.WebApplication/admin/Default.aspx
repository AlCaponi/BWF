<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<!--

There is no backdoor in this script.

                                                   _
                       /                            )
                      (                             |\
                     /|                              \\
                    //                                \\
                   ///                                 \|
                  /( \                                  )\
                  \\  \_                               //)
                   \\  :\__                           ///
                    \\     )                         // \
                     \\:  /                         // |/
                      \\ / \                       //  \
                       /)   \     ___..-'         ((  \_|
                      //     /  .'  _.'           \ \  \
                     /|       \(  _\_____          \ | /
                    (| _ _  __/          '-.       ) /.'
                     \\ .  '-.__ \          \_    / / \
                      \\_'.     > '-._   '.   \  / / /
                       \ \      \     \    \   .' /.'
                        \ \  '._ /     \  /   / .' |
                         \ \_     \_   |    .'_/ __/
                          \  \      \_ |   / /  _/ \_
                           \  \       / _.' /  /     \
                           \   |     /.'   / .'       '-,_
                            \   \  .'   _.'_/             \
               /\    /\      ) ___(    /_.'           \    |
              | _\__// \    (.'      _/               |    |
              \/_  __  /--'`    ,                   __/    /
              (_ ) /b)  \  '.   :            \___.-:_/ \__/
              /:/:  ,     ) :        (      /_.'_/-' |_ _ /
             /:/: __/\ >  __,_.----.__\    /        (/(/(/
            (_(,_/V .'/--'    _/  __/ |   /
             VvvV  //`    _.-' _.'     \   \
               n_n//     (((/->/        |   /
               '--'         ~='          \  |
                                          | |_,,,
                             snd          \  \  /
                                           '.__)

-->
<html>
	<head>
		<meta http-equiv="content-language" content="de" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<title>BWF: Login</title>
		<script type="text/javascript" src="/js/jquery.js"></script>
		<script type="text/javascript" src="/js/bootstrap.min.js"></script>
		<link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="/css/bootstrap-theme.css" />
		<link rel="stylesheet" type="text/css" href="/css/overrides.css" />
	</head>
	<body>
		<div class="jumbotron vertical-center">
			<div class="container login-form">
                <div class="img-responsive">
                    <img src="/images/Logo_ZSOEMME.png" alt="Logo" class="img-responsive" />
                </div>
    				<form method="post" action="./">
					<div class="form-group">
						<label for="uname">Benutzername</label>
						<input type="text" class="form-control" id="uname" name="uname" placeholder="Benutzername" />
					</div>
					<div class="form-group">
						<label for="upass">Passwort</label>
						<input type="password" class="form-control" id="upass" name="upass" placeholder="Passwort" />
					</div>
					<input type="submit" value="Login" class="btn btn-primary" />
				</form>
                <br />
                <%if (err) {%>
                    <div class="alert alert-danger" role="alert">Ungültiger Benutzername und/oder Passwort</div>
                <%} %>
			</div>
		</div>
	</body>
</html>
