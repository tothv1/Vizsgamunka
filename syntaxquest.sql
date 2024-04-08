-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 02. 12:51
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
-- Adatbázis: `syntaxquest`
--
CREATE DATABASE IF NOT EXISTS `syntaxquest` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `syntaxquest`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `achievements`
--

DROP TABLE IF EXISTS `achievements`;
CREATE TABLE `achievements` (
  `achievement_id` int(11) NOT NULL,
  `achievement_name` varchar(254) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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
CREATE TABLE `blacklisted_tokens` (
  `token_id` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `blacklisted_status_expires` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `blacklisted_tokens`
--

INSERT INTO `blacklisted_tokens` (`token_id`, `token`, `blacklisted_status_expires`) VALUES
('1683b6df-171a-4042-9a87-01bdb74bd036', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODg4ODM2LCJleHAiOjE3MTE5NzUyMzYsImlhdCI6MTcxMTg4ODgzNiwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.np1l6O19w2f49L__AZL4g-sGXz3VkUt3Pj9Pq9g5tYk', '2024-04-01 12:40:36'),
('1ee523c6-a130-482d-a1c8-2107d4df6f23', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODkwOTYxLCJleHAiOjE3MTE5NzczNjEsImlhdCI6MTcxMTg5MDk2MSwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.zR1QHHuE7m55dALhnOSPkr_ooWkbl_wqtkb6yr9TXI4', '2024-04-01 13:16:01'),
('28c584fd-c1f8-4c49-b404-6a85bc0c1888', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODg4OTYxLCJleHAiOjE3MTE5NzUzNjEsImlhdCI6MTcxMTg4ODk2MSwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.NDvjMyBPPYEJNdIF1Ve7aPDNTQFoJ8opIyjlr55wgkU', '2024-04-01 12:42:41'),
('7ca79b35-65ec-4527-9e10-3a670d4cdfb6', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODg4ODkzLCJleHAiOjE3MTE5NzUyOTMsImlhdCI6MTcxMTg4ODg5MywiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.o3CkYUXhOF8oQV7Yq41qn5C6pXjBmFl-cotCrQsp45k', '2024-04-01 12:41:33'),
('840d110d-055c-4c64-ae0b-2815597f107a', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODkwOTc4LCJleHAiOjE3MTE5NzczNzgsImlhdCI6MTcxMTg5MDk3OCwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.V71Oqat1KFxCnMIeV7QqvxLtpyCovchA40xK8DWs3YU', '2024-04-01 13:16:18'),
('98a89b35-9405-455e-8183-1e808f603e83', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODkwOTUxLCJleHAiOjE3MTE5NzczNTEsImlhdCI6MTcxMTg5MDk1MSwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.KJ0deD7IxymIzl6fvBmPsKKXcgRfzZsjVVbBY469mJM', '2024-04-01 13:15:51'),
('a0722b23-6083-4dbc-9ce0-38b0ed4864ac', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODkxNjcyLCJleHAiOjE3MTE5NzgwNzIsImlhdCI6MTcxMTg5MTY3MiwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.SKZ1JmRYtkW7Psmj0MGDfvR4kxO_F38TGVaaSo7Wqro', '2024-04-01 13:27:52'),
('db257bbd-6c27-497d-b86e-d9d435599275', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzExODkwOTMzLCJleHAiOjE3MTE5NzczMzMsImlhdCI6MTcxMTg5MDkzMywiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.3UuBq7maiK3sxEs_MfRfg4ZYl6_dfYcHluTVy5MisaQ', '2024-04-01 13:15:33');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `logged_in_users`
--

DROP TABLE IF EXISTS `logged_in_users`;
CREATE TABLE `logged_in_users` (
  `logged_is_users_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL,
  `username` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `sessionExpires` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `logged_in_users`
--

INSERT INTO `logged_in_users` (`logged_is_users_id`, `userid`, `username`, `token`, `sessionExpires`) VALUES
(10, 'abded31c-296b-4c1d-bd4c-96a0dade6c9c', 'vitya0717', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJhYmRlZDMxYy0yOTZiLTRjMWQtYmQ0Yy05NmEwZGFkZTZjOWMiLCJ1c2VyU3RhdElkIjoiNSIsInVzZXJuYW1lIjoidml0eWEwNzE3Iiwicm9sZSI6IkFkbWluIiwidXNlclJlZ2RhdGUiOiIwMy8zMC8yMDI0IDE4OjI3OjExIiwibmJmIjoxNzEyMDU0MDI4LCJleHAiOjE3MTIxNDA0MjgsImlhdCI6MTcxMjA1NDAyOCwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.L1btoGU-Zi0c_qob83x1LrfbovbPGp4IQT3V1fyztTE', '2024-04-03 10:33:48');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registered_users`
--

DROP TABLE IF EXISTS `registered_users`;
CREATE TABLE `registered_users` (
  `userid` varchar(254) NOT NULL,
  `username` varchar(64) NOT NULL,
  `fullname` varchar(254) NOT NULL,
  `email` varchar(64) NOT NULL,
  `hash` text NOT NULL,
  `is_logged_in` tinyint(1) NOT NULL,
  `regdate` datetime NOT NULL,
  `lastlogin` datetime DEFAULT NULL,
  `roleid` int(11) NOT NULL DEFAULT 2,
  `change_password_confirmation_key` text DEFAULT NULL
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
CREATE TABLE `registry` (
  `temp_userid` varchar(254) NOT NULL,
  `temp_username` varchar(64) NOT NULL,
  `temp_fullname` varchar(254) NOT NULL,
  `temp_email` varchar(64) NOT NULL,
  `temp_hash` text NOT NULL,
  `temp_regdate` datetime NOT NULL,
  `temp_roleid` int(11) DEFAULT NULL,
  `temp_user_expire` datetime NOT NULL,
  `temp_confirmation_key` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `roleid` int(11) NOT NULL,
  `role_name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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
CREATE TABLE `temp_roles` (
  `temp_role_id` int(11) NOT NULL,
  `role_name` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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
CREATE TABLE `user_achievements` (
  `user_achievement_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL,
  `achievement_id` int(11) NOT NULL,
  `achievement_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

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
CREATE TABLE `user_stats` (
  `user_stat_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL,
  `kills` int(11) NOT NULL,
  `deaths` int(11) NOT NULL,
  `highestKillCount` int(11) NOT NULL,
  `highestLevel` int(11) NOT NULL,
  `timesplayed` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_stats`
--

INSERT INTO `user_stats` (`user_stat_id`, `userid`, `kills`, `deaths`, `highestKillCount`, `highestLevel`, `timesplayed`) VALUES
(5, 'abded31c-296b-4c1d-bd4c-96a0dade6c9c', 0, 0, 0, 0, 0),
(7, '9855c870-1056-4090-be42-37650360319d', 5000, 121, 5001, 0, 30);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `achievements`
--
ALTER TABLE `achievements`
  ADD PRIMARY KEY (`achievement_id`),
  ADD UNIQUE KEY `achievement_name` (`achievement_name`);

--
-- A tábla indexei `blacklisted_tokens`
--
ALTER TABLE `blacklisted_tokens`
  ADD PRIMARY KEY (`token_id`);

--
-- A tábla indexei `logged_in_users`
--
ALTER TABLE `logged_in_users`
  ADD PRIMARY KEY (`logged_is_users_id`),
  ADD KEY `userid` (`userid`);

--
-- A tábla indexei `registered_users`
--
ALTER TABLE `registered_users`
  ADD PRIMARY KEY (`userid`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `username` (`username`),
  ADD KEY `roleid` (`roleid`);

--
-- A tábla indexei `registry`
--
ALTER TABLE `registry`
  ADD PRIMARY KEY (`temp_userid`),
  ADD UNIQUE KEY `temp_email` (`temp_email`),
  ADD UNIQUE KEY `temp_username` (`temp_username`),
  ADD KEY `temp_roleid` (`temp_roleid`);

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`roleid`),
  ADD UNIQUE KEY `role_name` (`role_name`);

--
-- A tábla indexei `temp_roles`
--
ALTER TABLE `temp_roles`
  ADD PRIMARY KEY (`temp_role_id`);

--
-- A tábla indexei `user_achievements`
--
ALTER TABLE `user_achievements`
  ADD PRIMARY KEY (`user_achievement_id`),
  ADD KEY `achievement_id` (`achievement_id`),
  ADD KEY `userid` (`userid`) USING BTREE;

--
-- A tábla indexei `user_stats`
--
ALTER TABLE `user_stats`
  ADD PRIMARY KEY (`user_stat_id`),
  ADD UNIQUE KEY `userid` (`userid`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `achievements`
--
ALTER TABLE `achievements`
  MODIFY `achievement_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `logged_in_users`
--
ALTER TABLE `logged_in_users`
  MODIFY `logged_is_users_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `roles`
--
ALTER TABLE `roles`
  MODIFY `roleid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT a táblához `temp_roles`
--
ALTER TABLE `temp_roles`
  MODIFY `temp_role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `user_achievements`
--
ALTER TABLE `user_achievements`
  MODIFY `user_achievement_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT a táblához `user_stats`
--
ALTER TABLE `user_stats`
  MODIFY `user_stat_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

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
