DROP DATABASE IF EXISTS `Restaurant`;
CREATE DATABASE `Restaurant`;
USE `Restaurant`;
CREATE TABLE `pendingUser` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `email` varchar(256) UNIQUE NOT NULL,
  `verificationCode` int(5) UNSIGNED NOT NULL,
  PRIMARY KEY (`id`)
);
CREATE TABLE `resetPassword` (
  `email` varchar(256) UNIQUE NOT NULL,
  `verificationCode` int(5) UNSIGNED NOT NULL,
  PRIMARY KEY (`email`)
);
CREATE TABLE `user` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `email` varchar(128) UNIQUE DEFAULT NULL,
  `name` varchar(128) DEFAULT NULL,
  `phone` varchar(25) DEFAULT NULL,
  `address` varchar(256) DEFAULT NULL,
  `role` varchar(256) DEFAULT 'customer',
  `passwordHash` varchar(512),
  `passwordSalt` varchar(5),
  `refreshToken` varchar(512) NOT NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE `item` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  `url` varchar(256) DEFAULT NULL,
  `category` varchar(20) NOT NULL,
  `price` real(10,2) NOT NULL,
  `description` varchar(1024) DEFAULT NULL,
  `isAvailable` BOOLEAN NOT NULL,
  PRIMARY KEY (`id`)
);
CREATE TABLE `order` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `userId` int(10) UNSIGNED NOT NULL,
  `itemId` int(10) UNSIGNED NOT NULL,
  `quantity` int(10) UNSIGNED NOT NULL,
  `status` varchar(20) NOT NULL,
  `requestDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `receiptDate` timestamp DEFAULT NULL,
  `deliveryCost` real(10,2) DEFAULT 0.0,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  FOREIGN KEY (`itemId`) REFERENCES `item` (`id`)
);
CREATE TABLE `weeklyMenu` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `itemId` int(10) UNSIGNED NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`itemId`) REFERENCES `item` (`id`)
);
CREATE TABLE `payment`(
`id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`orderId` int(10) UNSIGNED NOT NULL,
`userId` int(10) UNSIGNED NOT NULL,
`userName` varchar(50) NOT NULL,
`userAddress` varchar(50) NOT NULL,
`orderStatus` varchar(50) NOT NULL,
`paymentCost` real(10,2) DEFAULT 0.0,
PRIMARY KEY (`id`),
 FOREIGN KEY (`orderId`) REFERENCES `order` (`id`),
 FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
)
