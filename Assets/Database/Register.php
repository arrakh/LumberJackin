<?php 
    include_once("DBConnector.php");

    $user = $_POST['username'];
    $pass = $_POST['password'];
    $email = $_POST['email'];
    
    $input_data = "INSERT INTO login (username, password, email) VALUES ('". $user ."', '". $pass . "' , '". $email ."');";
    
    mysqli_query ($connection, $input_data);
?>