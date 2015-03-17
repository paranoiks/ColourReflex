<?php
/*
POST request parameters:
action - the action we want to perform (post_score)
score - the score we want to post if any
fb_id - the fb id of the player (should come encrypted, at least hashed)
*/

$servername = "localhost";
$database = "epicwork_colourreflex";
$username = "epicwork_colourr";
$password = "epicwork_colourReflex153";

$con = new mysqli($servername, $username, $password, $database);

if($con->connect_error)
{
	return;
}

$action = $_POST["action"];

if($action == "post_score")
{
	post_score();
}

function post_score()
{
	global $con;
	
	$score = $_POST["score"];
	if(is_numeric($score)
	{
		//if the score is a valid number, post it to the database
		$fb_id = $_POST["fb_id"];
		$query = "INSERT INTO score 
	}
	else	
	{
		//else return
		return;
	}
}

?>