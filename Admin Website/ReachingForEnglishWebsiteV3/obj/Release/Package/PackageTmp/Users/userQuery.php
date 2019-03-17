<?php
    include_once("colors.php");

    $pdo = new PDO($dsn, $user, $pass, $opt); 

            $stmt = $pdo->prepare('SELECT * FROM user');
            $stmt->execute();
            $result = json_encode($stmt->fetchAll(PDO::FETCH_ASSOC));
            echo($result); 
        
?>








