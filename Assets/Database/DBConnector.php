<?php
    $servername = "localhost";
    $database = "lumberjackin";
    $username = "root";
    $password = "";

    $connection = new mysqli($servername, $username, $password, $database);

    if($connection->connect_error){
        die("KONEKSI GAGAL". $connection->connect_error);
    } else {
        echo"KONEKSI BERHASIL";
        echo"\t";
    }
?>