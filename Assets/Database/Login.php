<?php 
    include_once("DBConnector.php");

    $user = $_POST['username'];
    $pass = $_POST['password'];
    
    $check_data = "SELECT username FROM login WHERE username ='" .$user. "';";
    $result = $connection->query($check_data);

    if($result->num_rows > 0){
        while($result->fetch_assoc()){
            echo "DATA SUDAH ADA, LOGIN BERHASIL";
        }
    }
    else{
        echo "DATA BELUM ADA";  
    }

    $connection->close();

?>