<?php 
session_start();
$downloadurl = "http://". $_SERVER['SERVER_NAME'] ."/QuizAppWebsite/".$_SESSION['lolzor'];
?>

<html>
	<head>
		<title>QuizApp - Make a Quiz</title>
		<script type ="text/javascript" src ="scripts/jquery-1.4.2.js"></script>
		<script type ="text/javascript" src = "scripts/jquery.cloneform.js"></script>
		<link rel = "stylesheet" href = "css/style.css" type = "text/css" media = "screen" />
	</head>

	<body>
		<div id = "wrapper">
			<div id = "header">
				<a href = "quizapp2.php"><img src = "images/quizlogo.png" id = "logo" alt="Create a Quiz"/></a>
			</div>
			<div id = "main">
			<center>
			<h1>Quiz Created!</h1>
			</center>
			<p>Your quiz has been created successfully. It can now be accessed through the Quiz App on your Android device. </p>
			<center>
				<a href = "<?php echo $downloadurl;?>" download="<?php echo $_SESSION['lolzor2'];?>">Download Local Copy</a><br />
				<a href = "quizapp2.php">Create Another Quiz</a>
			</center>
			<br />

			</div>
			<div id = "footer">
				&copy; Simon Lomax
			</div>
	</body>
</html>
<script type = "text/javascript">
	function downloadFile()
	{
		return "<?php echo $_SESSION['lolzor']?>";
	}
</script>
<?php
session_destroy();
?>