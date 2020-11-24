<?php
    include_once("DBConnector.php");
    include 'CheckCode.php';
    
    $newpass = $_POST['newPass'];
    $code = $_POST['kode'];

    $input_data = "UPDATE login SET password = '$newpass' WHERE kode_verifikasi = '$code'";
    mysqli_query ($connection, $input_data);
?>