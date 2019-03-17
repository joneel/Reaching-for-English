<?php
    include_once("colors.php");
    
    $pdo = new PDO($dsn, $user, $pass, $opt); 
        $id=$_GET['userType'];
        $id2 = $_GET['env']; 
        $stmt = $pdo->prepare('SELECT DISTINCT tid FROM lessons WHERE userType = ? and env = ?');
        $stmt->execute([$id, $id2]);
        $result = json_encode($stmt->fetchAll(PDO::FETCH_ASSOC));
        echo($result); 
    
?>




