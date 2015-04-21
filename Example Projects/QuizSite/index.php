<?php
session_start();

if($_SERVER["REQUEST_METHOD"] == "POST")
{
	$quizLength = "";
	$quizname =  "";
	
	$quizname = test_input($_POST["quiz_name"]);
	$quizname = preg_replace('/\s+/', '',$quizname);
	$_SESSION['lolzor'] = "quizes/".$quizname.".xml";
	$_SESSION['lolzor2'] = $quizname;
	$quizLength = test_input($_POST["quizLength"]);
	$length = intval($quizLength);
	
	$xml = new DOMDocument("1.0");
	$root = $xml -> createElement("quiz");
	$xml->appendChild($root);
	
	for($i=1; $i <= $length; $i++)
	{
		$id = $i - 1;
		
		$question = $answer1 = $answer2 = $answer3 = $correct = "";
		$question = test_input($_POST["question_".$i]);
		$answer1 = test_input($_POST["answer1_".$i]);
		$answer2 = test_input($_POST["answer2_".$i]);
		$answer3 = test_input($_POST["answer3_".$i]);
		$correct = test_input($_POST["answers_".$i]);
		
		$xquestion = $xml->createElement("question");
		$xquestion->setAttribute('id', $id);
		$xquestion->setAttribute('text', $question);
		$xanswers = $xml->createElement("answers");
		$xanswer1 = $xml->createElement("answer");
		if($correct == 1)
		{
			if($i == $length)
			{
				$xanswer1->setAttribute('action', 'end');
			}
			else
			{
				$xanswer1->setAttribute('action', "true");
			}
		}
		else if ($correct != 1)
		{
			if($i == $length)
			{
				$xanswer1->setAttribute('action', 'endfalse');
			}
			else
			{
				$xanswer1->setAttribute("action", "false");
			}
		}
		$xanswer1Text = $xml->createTextNode($answer1);
		$xanswer1->appendChild($xanswer1Text);
		
		$xanswer2 = $xml->createElement("answer");
		if($correct == 2)
		{
			if($i == $length)
			{
				$xanswer2->setAttribute('action', 'end');
			}
			else
			{
				$xanswer2->setAttribute('action', "true");
			}
		}
		else if ($correct != 2)
		{
			if($i == $length)
			{
				$xanswer2->setAttribute('action', 'endfalse');
			}
			else{
			$xanswer2->setAttribute("action", "false");
			}
		}
		$xanswer2Text = $xml->createTextNode($answer2);
		$xanswer2->appendChild($xanswer2Text);
		
		$xanswer3 = $xml->createElement("answer");
		if($correct == 3)
		{
			if($i == $length)
			{
				$xanswer3->setAttribute('action', 'end');
			}
			else
			{
				$xanswer3->setAttribute('action', "true");
			}
		}
		else if ($correct != 3)
		{
			if($i == $length)
			{
				$xanswer3->setAttribute('action', 'endfalse');
			}
			else
			{
				$xanswer3->setAttribute("action", "false");
			}
		}
		$xanswer3Text = $xml->createTextNode($answer3);
		$xanswer3->appendChild($xanswer3Text);
		
		$xanswers->appendChild($xanswer1);
		$xanswers->appendChild($xanswer2);
		$xanswers->appendChild($xanswer3);
		$xquestion->appendChild($xanswers);
		$root->appendChild($xquestion);

	}
	
	$xml->formatOutput = true;
	$xml->saveXML();
	$xml->save("quizes/".$quizname.".xml") or die ("Error");
	header('Location: thanks.php');
}


function test_input($data)
{
	$data = trim($data);
	$data = stripslashes($data);
	$data = htmlspecialchars($data);
	return $data;
}
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
				<form id="quiz" method = "POST">
					<center><label>Quiz Name</label>
					<br/><input type = "text" name = "quiz_name" required/></center>
					<fieldset class="clonable">
						<legend>Question</legend>
						<table>
						<tr><td><label>Question</label></td><td><input type = "text" id ="question" name = "question_1" required /></td></tr>
						<tr><td><label>Answer 1</label></td><td><input type = "text" name ="answer1_1" required /></td></tr>
						<tr><td><label>Answer 2</label></td><td><input type = "text" name ="answer2_1" required /></td></tr>
						<tr><td><label>Answer 3</label></td><td><input type = "text" name="answer3_1"  required/></td></tr>
						<tr><td><label>Correct Answer</label></td><td><input type ="radio" name ="answers_1" value ="1" required>1<input type ="radio" name ="answers_1" value ="2">2<input type ="radio" name ="answers_1" value ="3">3</td></tr>
						</table>
					</fieldset>
					<div id="formbuttons">
					<input type = "hidden" value = "1" name = "quizLength" />
					<input type="button" class="button" id="clonetrigger" value = "Add Question" onclick="getLength()" />
					<input type ="submit" class ="button" id="submit" value = "Create Quiz" />
				</form>
			</div>
			<div id = "footer">
				&copy; Simon Lomax
			</div>
	</body>
	<script type = "text/javascript">
		var count = 1;
		var MAX = 5;
		function getLength()
		{
			$(document).ready(function() 
			{
				count++;
				$('input:hidden[name="quizLength"]').val(count);
			
			});
			
		}
		
		$('#quiz').submit(function(){
			if(count < 2)
			{
				alert('Quiz must be longer than 1 question.');
				return false;
			}
			else
			{
				return true;
			}
		});
	</script>
</html>