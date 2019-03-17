<?php
    include_once("colors.php");
    
    $pdo = new PDO($dsn, $user, $pass, $opt); 
        $id=$_GET['filename'];
        $stmt = $pdo->prepare('SELECT text FROM lessons WHERE filename = ?');
        $stmt->execute([$id]);
        $result = json_encode($stmt->fetchAll(PDO::FETCH_ASSOC));
        echo($result); 
    
?>






