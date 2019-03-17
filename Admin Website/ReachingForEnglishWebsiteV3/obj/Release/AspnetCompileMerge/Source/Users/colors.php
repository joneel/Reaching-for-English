<?php
    $host = 'mysql5012.site4now.net';
    $db   = 'db_a3acac_engdata';
    $user = 'a3acac_engdata';
    $pass = 'Nicaragua2017';
    $charset = 'utf8mb4';

    $dsn = "mysql:host=$host;dbname=$db;charset=$charset";
    $opt = [
        PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        PDO::ATTR_EMULATE_PREPARES   => false,
    ];
?>




