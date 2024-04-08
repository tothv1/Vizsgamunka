-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Már 31. 13:42
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `auth`
--
CREATE DATABASE IF NOT EXISTS `syntaxquest` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `syntaxquest`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `achievements`
--

DROP TABLE IF EXISTS `achievements`;
CREATE TABLE IF NOT EXISTS `achievements` (
  `achievement_id` int(11) NOT NULL AUTO_INCREMENT,
  `achievement_name` varchar(254) NOT NULL,
  PRIMARY KEY (`achievement_id`),
  UNIQUE KEY `achievement_name` (`achievement_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `achievements`
--

INSERT INTO `achievements` (`achievement_id`, `achievement_name`) VALUES
(4, 'First time died'),
(1, 'Firt time playing');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `blacklisted_tokens`
--

DROP TABLE IF EXISTS `blacklisted_tokens`;
CREATE TABLE IF NOT EXISTS `blacklisted_tokens` (
  `token_id` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `blacklisted_status_expires` datetime NOT NULL,
  PRIMARY KEY (`token_id`),
  UNIQUE KEY `token` (`token`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `logged_in_users`
--

DROP TABLE IF EXISTS `logged_in_users`;
CREATE TABLE IF NOT EXISTS `logged_in_users` (
  `logged_is_users_id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(254) NOT NULL,
  `username` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `sessionExpires` datetime DEFAULT NULL,
  PRIMARY KEY (`logged_is_users_id`),
  KEY `userid` (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registered_users`
--

DROP TABLE IF EXISTS `registered_users`;
CREATE TABLE IF NOT EXISTS `registered_users` (
  `userid` varchar(254) NOT NULL,
  `username` varchar(64) NOT NULL,
  `fullname` varchar(254) NOT NULL,
  `email` varchar(64) NOT NULL,
  `hash` text NOT NULL,
  `is_logged_in` tinyint(1) NOT NULL,
  `regdate` datetime NOT NULL,
  `lastlogin` datetime DEFAULT NULL,
  `roleid` int(11) NOT NULL DEFAULT 2,
  `change_password_confirmation_key` text DEFAULT NULL,
  PRIMARY KEY (`userid`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `username` (`username`),
  KEY `roleid` (`roleid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `registered_users`
--

INSERT INTO `registered_users` (`userid`, `username`, `fullname`, `email`, `hash`, `is_logged_in`, `regdate`, `lastlogin`, `roleid`, `change_password_confirmation_key`) VALUES
('9855c870-1056-4090-be42-37650360319d', 'vitya0718', 'vitya', 'tviktor20000717@gmail.com', '$2a$11$R9eJx/ivRhKloLRB1zoFgesUxP9Uz2YIYFz5CjKbtg6kBHD0AfJom', 0, '2024-03-31 10:22:31', NULL, 2, NULL),
('abded31c-296b-4c1d-bd4c-96a0dade6c9c', 'vitya0717', 'Tóth Viktor', 'tothv@kkszki.hu', '$2a$11$dhHcE3PHHMahro54DFsx1eRN1UTOhgV8YQlXp.2nIJhraL.dgNhda', 1, '2024-03-30 18:27:11', NULL, 1, NULL);

--
-- Eseményindítók `registered_users`
--
DROP TRIGGER IF EXISTS `deleteUserFromLoggedIn`;
DELIMITER $$
CREATE TRIGGER `deleteUserFromLoggedIn` AFTER DELETE ON `registered_users` FOR EACH ROW DELETE FROM logged_in_users WHERE logged_in_users.userid = OLD.userid
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registry`
--

DROP TABLE IF EXISTS `registry`;
CREATE TABLE IF NOT EXISTS `registry` (
  `temp_userid` varchar(254) NOT NULL,
  `temp_username` varchar(64) NOT NULL,
  `temp_fullname` varchar(254) NOT NULL,
  `temp_email` varchar(64) NOT NULL,
  `temp_hash` text NOT NULL,
  `temp_regdate` datetime NOT NULL,
  `temp_roleid` int(11) DEFAULT NULL,
  `temp_user_expire` datetime NOT NULL,
  `temp_confirmation_key` text NOT NULL,
  PRIMARY KEY (`temp_userid`),
  UNIQUE KEY `temp_email` (`temp_email`),
  UNIQUE KEY `temp_username` (`temp_username`),
  KEY `temp_roleid` (`temp_roleid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `roleid` int(11) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(64) NOT NULL,
  PRIMARY KEY (`roleid`),
  UNIQUE KEY `role_name` (`role_name`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`roleid`, `role_name`) VALUES
(1, 'Admin'),
(2, 'User'),
(4, 'Vip');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `temp_roles`
--

DROP TABLE IF EXISTS `temp_roles`;
CREATE TABLE IF NOT EXISTS `temp_roles` (
  `temp_role_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(16) NOT NULL,
  PRIMARY KEY (`temp_role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `temp_roles`
--

INSERT INTO `temp_roles` (`temp_role_id`, `role_name`) VALUES
(1, 'Temp');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_achievements`
--

DROP TABLE IF EXISTS `user_achievements`;
CREATE TABLE IF NOT EXISTS `user_achievements` (
  `user_achievement_id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(254) NOT NULL,
  `achievement_id` int(11) NOT NULL,
  `achievement_date` datetime NOT NULL,
  PRIMARY KEY (`user_achievement_id`),
  KEY `achievement_id` (`achievement_id`),
  KEY `userid` (`userid`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_achievements`
--

INSERT INTO `user_achievements` (`user_achievement_id`, `userid`, `achievement_id`, `achievement_date`) VALUES
(6, '9855c870-1056-4090-be42-37650360319d', 4, '2024-03-31 13:38:50'),
(7, 'abded31c-296b-4c1d-bd4c-96a0dade6c9c', 4, '2024-03-31 13:38:50');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_stats`
--

DROP TABLE IF EXISTS `user_stats`;
CREATE TABLE IF NOT EXISTS `user_stats` (
  `user_stat_id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(254) NOT NULL,
  `kills` int(11) NOT NULL,
  `deaths` int(11) NOT NULL,
  `highestKillCount` int(11) NOT NULL,
  `highestLevel` int(11) NOT NULL,
  `timesplayed` int(11) NOT NULL,
  PRIMARY KEY (`user_stat_id`),
  UNIQUE KEY `userid` (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_stats`
--

INSERT INTO `user_stats` (`user_stat_id`, `userid`, `kills`, `deaths`, `highestKillCount`, `highestLevel`, `timesplayed`) VALUES
(5, 'abded31c-296b-4c1d-bd4c-96a0dade6c9c', 0, 0, 0, 0, 0),
(7, '9855c870-1056-4090-be42-37650360319d', 0, 0, 0, 0, 0);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `logged_in_users`
--
ALTER TABLE `logged_in_users`
  ADD CONSTRAINT `logged_in_users_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `registered_users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `registered_users`
--
ALTER TABLE `registered_users`
  ADD CONSTRAINT `registered_users_ibfk_2` FOREIGN KEY (`roleid`) REFERENCES `roles` (`roleid`);

--
-- Megkötések a táblához `registry`
--
ALTER TABLE `registry`
  ADD CONSTRAINT `registry_ibfk_1` FOREIGN KEY (`temp_roleid`) REFERENCES `temp_roles` (`temp_role_id`);

--
-- Megkötések a táblához `user_achievements`
--
ALTER TABLE `user_achievements`
  ADD CONSTRAINT `user_achievements_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `registered_users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `user_achievements_ibfk_2` FOREIGN KEY (`achievement_id`) REFERENCES `achievements` (`achievement_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `user_stats`
--
ALTER TABLE `user_stats`
  ADD CONSTRAINT `user_stats_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `registered_users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE;

DELIMITER $$
--
-- Események
--
DROP EVENT IF EXISTS `deleteExpiredTempUsers`$$
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTempUsers` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Delete expired temp users' DO DELETE FROM registry WHERE registry.temp_user_expire < NOW()$$

DROP EVENT IF EXISTS `deleteExpiredTokens`$$
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTokens` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Clears out sessions table each hour.' DO DELETE FROM blacklisted_tokens WHERE blacklisted_tokens.blacklisted_status_expires < NOW()$$

DROP EVENT IF EXISTS `deleteExpiredLoggedInUsers`$$
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredLoggedInUsers` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE DO DELETE FROM logged_in_users WHERE logged_in_users.sessionExpires < NOW()$$

DELIMITER ;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
