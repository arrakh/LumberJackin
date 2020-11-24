<?php
    include_once("DBConnector.php");

    $code = $_POST['kode'];

    $check_data = "SELECT * FROM login WHERE kode_verifikasi = '$code'";
    $result = $connection->query($check_data);

    if($result->num_rows > 0){
        while($result->fetch_assoc()){
            echo "DATA ADA";
        }
    }
    else{
        echo "error, DATA TIDAK DITEMUKAN";  
    }
?>