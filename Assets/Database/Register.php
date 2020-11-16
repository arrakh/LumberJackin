<?php 
    include_once("DBConnector.php");

    $user = $_POST['username'];
    $pass = $_POST['password'];
    
    $input_data = "INSERT INTO login (username, password) VALUES ('". $user ."', '". $pass . "');";
    
    mysqli_query ($connection, $input_data);
?>