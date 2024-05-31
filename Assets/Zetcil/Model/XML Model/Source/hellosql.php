<?php
	$host = $_GET['host'];
	$user = $_GET['username'];
	$pass = $_GET['password'];
	$database = $_GET['database'];
	$sql = $_GET['sql'];
	try 
	{
		$conn = mysqli_connect($host, $user, $pass, $database);
		$result = mysqli_query($conn, $sql);
		$rows = array();
		while ($r = mysqli_fetch_assoc($result)){
			$rows[] = $r;	
		}
		echo json_encode($rows);
	} catch (exception $e) {
		echo $e;
	}
?>