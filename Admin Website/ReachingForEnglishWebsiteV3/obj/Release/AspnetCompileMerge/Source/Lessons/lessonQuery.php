<?php
    include_once("colors.php");
    
    $pdo = new PDO($dsn, $user, $pass, $opt); 
        $id=$_GET['userType'];
        $id2 = $_GET['env']; 
        $id3 = $_GET['tid']; 
        $stmt = $pdo->prepare('SELECT lid,text,path,filename,ext FROM lessons WHERE userType = ? and env = ? and tid = ?');
        $stmt->execute([$id, $id2, $id3]);
        $result = json_encode($stmt->fetchAll(PDO::FETCH_ASSOC));
        echo($result); 
    
?>




