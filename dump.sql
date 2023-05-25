CREATE DATABASE  IF NOT EXISTS `railway` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `railway`;
-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: railway
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `directions`
--

DROP TABLE IF EXISTS `directions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `directions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `train` int NOT NULL,
  `from` varchar(100) NOT NULL,
  `to` varchar(100) NOT NULL,
  `departure_date` datetime NOT NULL,
  `arrival_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `directions_train_fk_idx` (`train`),
  CONSTRAINT `directions_train_fk` FOREIGN KEY (`train`) REFERENCES `train` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `directions`
--

LOCK TABLES `directions` WRITE;
/*!40000 ALTER TABLE `directions` DISABLE KEYS */;
INSERT INTO `directions` VALUES (1,1,'Йошкар-Ола','Москва','2022-05-18 18:22:00','2022-05-19 13:36:00'),(2,1,'asdasd','asdasd','2022-05-19 19:37:38','2022-05-19 19:37:38'),(3,1,'asdasd','asdasd','2022-05-19 19:37:38','2022-05-19 19:37:38'),(4,1,'asdasd','asdasd','2022-05-19 19:37:38','2022-05-19 19:37:38'),(8,1,'фывфыв','фывфыв','2022-05-20 00:33:12','2022-05-20 00:33:12');
/*!40000 ALTER TABLE `directions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `position`
--

DROP TABLE IF EXISTS `position`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `position` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `position`
--

LOCK TABLES `position` WRITE;
/*!40000 ALTER TABLE `position` DISABLE KEYS */;
INSERT INTO `position` VALUES (1,'Администратор'),(2,'Начальник'),(3,'Сотрудник');
/*!40000 ALTER TABLE `position` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stuff`
--

DROP TABLE IF EXISTS `stuff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stuff` (
  `id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(50) NOT NULL,
  `position` int NOT NULL,
  `password` varchar(45) NOT NULL,
  `username` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `nickname_UNIQUE` (`username`),
  KEY `stuff_position_idx` (`position`),
  CONSTRAINT `stuff_position` FOREIGN KEY (`position`) REFERENCES `position` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stuff`
--

LOCK TABLES `stuff` WRITE;
/*!40000 ALTER TABLE `stuff` DISABLE KEYS */;
INSERT INTO `stuff` VALUES (2,'Иванов Иван Иванович',2,'2c42e5cf1cdbafea04ed267018ef1511','ivan'),(3,'  ',1,'d41d8cd98f00b204e9800998ecf8427e','');
/*!40000 ALTER TABLE `stuff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `train`
--

DROP TABLE IF EXISTS `train`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `train` (
  `id` int NOT NULL AUTO_INCREMENT,
  `number` varchar(11) NOT NULL,
  `wagons_quantity` int NOT NULL,
  `type` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `number_UNIQUE` (`number`),
  KEY `train_train_type_fk_idx` (`type`),
  CONSTRAINT `train_train_type_fk` FOREIGN KEY (`type`) REFERENCES `train_type` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `train`
--

LOCK TABLES `train` WRITE;
/*!40000 ALTER TABLE `train` DISABLE KEYS */;
INSERT INTO `train` VALUES (1,'2132232',12,1);
/*!40000 ALTER TABLE `train` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `train_stuff`
--

DROP TABLE IF EXISTS `train_stuff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `train_stuff` (
  `id` int NOT NULL AUTO_INCREMENT,
  `train` int NOT NULL,
  `shift` enum('Первая','Вторая','Третья') NOT NULL,
  `stuff` int NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `comment` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `train_staff_train_fk_idx` (`train`),
  KEY `train_stuff_stuff_fk_idx` (`stuff`),
  CONSTRAINT `train_stuff_stuff_fk` FOREIGN KEY (`stuff`) REFERENCES `stuff` (`id`),
  CONSTRAINT `train_stuff_train_fk` FOREIGN KEY (`train`) REFERENCES `train` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `train_stuff`
--

LOCK TABLES `train_stuff` WRITE;
/*!40000 ALTER TABLE `train_stuff` DISABLE KEYS */;
INSERT INTO `train_stuff` VALUES (3,1,'Третья',2,'2022-05-20 22:20:00','2022-05-20 22:30:00','asdasdasdasd'),(10,1,'Вторая',2,'2022-05-20 00:25:09','2022-05-20 00:25:09',''),(11,1,'Вторая',2,'2022-05-20 00:25:13','2022-05-20 00:25:13','');
/*!40000 ALTER TABLE `train_stuff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `train_type`
--

DROP TABLE IF EXISTS `train_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `train_type` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `train_type`
--

LOCK TABLES `train_type` WRITE;
/*!40000 ALTER TABLE `train_type` DISABLE KEYS */;
INSERT INTO `train_type` VALUES (1,'Пассажирский'),(2,'Грузовой');
/*!40000 ALTER TABLE `train_type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-20  0:51:32
