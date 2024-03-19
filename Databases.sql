-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Már 19. 08:33
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

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
DROP DATABASE IF EXISTS `auth`;
CREATE DATABASE IF NOT EXISTS `auth` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `auth`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `blacklisted_tokens`
--

DROP TABLE IF EXISTS `blacklisted_tokens`;
CREATE TABLE `blacklisted_tokens` (
  `token_id` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `blacklisted_status_expires` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `blacklisted_tokens`
--

INSERT INTO `blacklisted_tokens` (`token_id`, `token`, `blacklisted_status_expires`) VALUES
('0d6b78e6-898a-44aa-ae31-1b30033b4905', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDc0NzI2NCwiZXhwIjoxNzEwODMzNjY0LCJpYXQiOjE3MTA3NDcyNjQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.6QBJQmZjqtXFZQQFLksfFlqd1vsF_0CuC-F4jBgnUzQ', '2024-03-19 07:34:24'),
('3536faf1-72f5-450b-9409-c4f4b23a7538', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJWaXAiLCJ1c2VyUmVnZGF0ZSI6IjAyLzI3LzIwMjQgMTc6MDU6NTgiLCJuYmYiOjE3MTA3NDYwMjMsImV4cCI6MTcxMDgzMjQyMywiaWF0IjoxNzEwNzQ2MDIzLCJpc3MiOiJhdXRoLWFwaSIsImF1ZCI6ImF1dGgtY2xpZW50In0.btM5SM17mieeCzFvD60dfXqhiYojL1_XMddY1O6QS7s', '2024-03-19 07:13:43'),
('95c5fb09-1591-4af9-be34-fbbfaa5f3150', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDY5NTg0NSwiZXhwIjoxNzEwNzgyMjQ1LCJpYXQiOjE3MTA2OTU4NDUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.vNBSDgoue2p_taOHgYilMbmR1lDl891Cz-xSn55gDGo', '2024-03-18 17:17:25'),
('d7798692-0daf-4559-ad43-e30742b781cf', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJWaXAiLCJ1c2VyUmVnZGF0ZSI6IjAyLzI3LzIwMjQgMTc6MDU6NTgiLCJuYmYiOjE3MTA3NDcyMzIsImV4cCI6MTcxMDgzMzYzMiwiaWF0IjoxNzEwNzQ3MjMyLCJpc3MiOiJhdXRoLWFwaSIsImF1ZCI6ImF1dGgtY2xpZW50In0.AfVojgzs9UlmdH4ccoyoELGAFvJcrB1u5toE3TPnTJY', '2024-03-19 07:33:52'),
('d7ad8fed-4a86-4c15-8823-326de75246d8', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI0MmU1NTA3Yi1hOGExLTQyMDYtYjY4Mi02OTI5NDIxMzA5MWQiLCJ1c2VybmFtZSI6InZpdHlhMDcxOSIsInJvbGUiOiJVc2VyIiwidXNlclJlZ2RhdGUiOiIwMy8xOC8yMDI0IDA3OjE1OjA4IiwibmJmIjoxNzEwNzQ2NTkwLCJleHAiOjE3MTA4MzI5OTAsImlhdCI6MTcxMDc0NjU5MCwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.Uia7FcNJfMfmirL8nbG5azoJjEVPLFsChmqLw6hlUns', '2024-03-19 07:23:10'),
('df4af43b-6985-4589-8abe-fdaed9c83163', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDc0NzM3OSwiZXhwIjoxNzEwODMzNzc5LCJpYXQiOjE3MTA3NDczNzksImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.pbHUJKL3dwf4ifZY7GrmHASXLYD2lN4TklLFw2Y0-i0', '2024-03-19 07:36:19'),
('f9e5816b-8240-49f3-b8cd-3ef09090af92', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI0MmU1NTA3Yi1hOGExLTQyMDYtYjY4Mi02OTI5NDIxMzA5MWQiLCJ1c2VybmFtZSI6InZpdHlhMDcxOSIsInJvbGUiOiJVc2VyIiwidXNlclJlZ2RhdGUiOiIwMy8xOC8yMDI0IDA3OjE1OjA4IiwibmJmIjoxNzEwNzQ2MTg0LCJleHAiOjE3MTA4MzI1ODQsImlhdCI6MTcxMDc0NjE4NCwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.MQQmNT3NfZ_ABQZoBgUR4MfQfMLoupar8jTaCja75GM', '2024-03-19 07:16:24');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `logged_in_users`
--

DROP TABLE IF EXISTS `logged_in_users`;
CREATE TABLE `logged_in_users` (
  `userid` varchar(254) NOT NULL,
  `token` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `logged_in_users`
--

INSERT INTO `logged_in_users` (`userid`, `token`) VALUES
('42e5507b-a8a1-4206-b682-69294213091d', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI0MmU1NTA3Yi1hOGExLTQyMDYtYjY4Mi02OTI5NDIxMzA5MWQiLCJ1c2VybmFtZSI6InZpdHlhMDcxOSIsInJvbGUiOiJVc2VyIiwidXNlclJlZ2RhdGUiOiIwMy8xOC8yMDI0IDA3OjE1OjA4IiwibmJmIjoxNzEwNzQ2ODI3LCJleHAiOjE3MTA4MzMyMjcsImlhdCI6MTcxMDc0NjgyNywiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.zc-3GfKQofghb63Pj-ito69AiXtWY7lgwbo7RINTPzE'),
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDc0NzU0NywiZXhwIjoxNzEwODMzOTQ3LCJpYXQiOjE3MTA3NDc1NDcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.CoGnRaQOUeYta9_vOVkNTxklKUzJk1SqLLDaWad1dYU');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registered_users`
--

DROP TABLE IF EXISTS `registered_users`;
CREATE TABLE `registered_users` (
  `userid` varchar(254) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `username` varchar(64) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `fullname` varchar(254) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `email` varchar(64) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `hash` text CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `is_logged_in` tinyint(1) NOT NULL,
  `regdate` datetime NOT NULL,
  `roleid` int(11) NOT NULL DEFAULT 2,
  `change_password_confirmation_key` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `registered_users`
--

INSERT INTO `registered_users` (`userid`, `username`, `fullname`, `email`, `hash`, `is_logged_in`, `regdate`, `roleid`, `change_password_confirmation_key`) VALUES
('42e5507b-a8a1-4206-b682-69294213091d', 'vitya0718', 'Tóth Viktor', 'xdddd@gmail.hu', '$2a$11$MyJu5prEL6ALNKCBFhF2V.2AGb2pV2a8YKSZJZGKyqPhhWkYOb.be', 1, '2024-03-18 07:15:08', 2, NULL),
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'senki', 'tothv@kkszki.hu', '$2a$11$O8K/izNpTKxIFIudTNPNZe0LOyoTpsC13dEXjeut6CZSazQJyRt3W', 1, '2024-02-27 17:05:58', 1, NULL);

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
  `temp_username` varchar(64) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `temp_fullname` varchar(254) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `temp_email` varchar(64) CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `temp_hash` text CHARACTER SET utf8 COLLATE utf8_hungarian_ci NOT NULL,
  `temp_regdate` datetime NOT NULL,
  `temp_roleid` int(11) DEFAULT NULL,
  `temp_user_expire` datetime NOT NULL,
  `temp_confirmation_key` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `roleid` int(11) NOT NULL,
  `role_name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `temp_roles`
--

INSERT INTO `temp_roles` (`temp_role_id`, `role_name`) VALUES
(1, 'Temp');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `blacklisted_tokens`
--
ALTER TABLE `blacklisted_tokens`
  ADD PRIMARY KEY (`token_id`),
  ADD UNIQUE KEY `token` (`token`) USING HASH;

--
-- A tábla indexei `logged_in_users`
--
ALTER TABLE `logged_in_users`
  ADD PRIMARY KEY (`userid`);

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
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `roles`
--
ALTER TABLE `roles`
  MODIFY `roleid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `temp_roles`
--
ALTER TABLE `temp_roles`
  MODIFY `temp_role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Megkötések a kiírt táblákhoz
--

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

DELIMITER $$
--
-- Események
--
DROP EVENT IF EXISTS `deleteExpiredTempUsers`$$
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTempUsers` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Delete expired temp users' DO DELETE FROM registry WHERE registry.temp_user_expire < NOW()$$

DROP EVENT IF EXISTS `deleteExpiredTokens`$$
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTokens` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Clears out sessions table each hour.' DO DELETE FROM blacklisted_tokens WHERE blacklisted_tokens.blacklisted_status_expires < NOW()$$

DELIMITER ;
--
-- Adatbázis: `game`
--
DROP DATABASE IF EXISTS `game`;
CREATE DATABASE IF NOT EXISTS `game` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `game`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `achievements`
--

DROP TABLE IF EXISTS `achievements`;
CREATE TABLE `achievements` (
  `id` int(11) NOT NULL,
  `achievement_name` char(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `achievements`
--

INSERT INTO `achievements` (`id`, `achievement_name`) VALUES
(1, 'First time play'),
(2, 'First time xd'),
(3, 'First Kxdd'),
(4, 'xddd'),
(5, 'string');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `id` int(11) NOT NULL,
  `role_name` char(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`id`, `role_name`) VALUES
(1, 'Admin'),
(2, 'User'),
(4, 'Vip');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` varchar(254) NOT NULL,
  `username` char(32) NOT NULL,
  `email` char(64) NOT NULL,
  `regdate` datetime NOT NULL,
  `lastlogin` datetime NOT NULL,
  `roleid` int(11) NOT NULL,
  `user_stats_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`id`, `username`, `email`, `regdate`, `lastlogin`, `roleid`, `user_stats_id`) VALUES
('42e5507b-a8a1-4206-b682-69294213091d', 'vitya0718', 'xdddd@gmail.hu', '2024-03-18 07:15:08', '2024-03-18 08:27:07', 2, 3),
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'tothv@kkszki.hu', '2024-02-27 17:05:58', '2024-03-18 08:39:07', 1, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userstats`
--

DROP TABLE IF EXISTS `userstats`;
CREATE TABLE `userstats` (
  `user_stat_id` int(11) NOT NULL,
  `kills` int(11) NOT NULL,
  `highestKillCount` int(11) NOT NULL,
  `deaths` int(11) NOT NULL,
  `highestLevel` int(11) NOT NULL,
  `timesplayed` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `userstats`
--

INSERT INTO `userstats` (`user_stat_id`, `kills`, `highestKillCount`, `deaths`, `highestLevel`, `timesplayed`) VALUES
(2, 500000, 0, 20, 0, 0),
(3, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_achievements`
--

DROP TABLE IF EXISTS `user_achievements`;
CREATE TABLE `user_achievements` (
  `achievement_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_achievements`
--

INSERT INTO `user_achievements` (`achievement_id`, `userid`) VALUES
(13, '42e5507b-a8a1-4206-b682-69294213091d'),
(14, '42e5507b-a8a1-4206-b682-69294213091d'),
(11, 'bb0d5d3d-95ed-4243-a836-a0ed0e54d032'),
(12, 'bb0d5d3d-95ed-4243-a836-a0ed0e54d032');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_achievement_details`
--

DROP TABLE IF EXISTS `user_achievement_details`;
CREATE TABLE `user_achievement_details` (
  `achievement_detail_id` int(11) NOT NULL,
  `achievement_id` int(11) NOT NULL,
  `user_achievement_id` int(11) NOT NULL,
  `achievement_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_achievement_details`
--

INSERT INTO `user_achievement_details` (`achievement_detail_id`, `achievement_id`, `user_achievement_id`, `achievement_date`) VALUES
(7, 4, 11, '2024-02-28 20:13:15'),
(8, 5, 12, '2024-02-28 20:16:39'),
(9, 1, 13, '2024-03-18 09:42:20'),
(10, 2, 14, '2024-03-18 09:42:20');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `achievements`
--
ALTER TABLE `achievements`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `username` (`username`),
  ADD KEY `User_fk0` (`roleid`),
  ADD KEY `user_stats_id` (`user_stats_id`);

--
-- A tábla indexei `userstats`
--
ALTER TABLE `userstats`
  ADD PRIMARY KEY (`user_stat_id`);

--
-- A tábla indexei `user_achievements`
--
ALTER TABLE `user_achievements`
  ADD PRIMARY KEY (`achievement_id`),
  ADD KEY `userid` (`userid`);

--
-- A tábla indexei `user_achievement_details`
--
ALTER TABLE `user_achievement_details`
  ADD PRIMARY KEY (`achievement_detail_id`),
  ADD KEY `UserAchievements_fk0` (`achievement_id`),
  ADD KEY `achi_connect_id` (`user_achievement_id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `achievements`
--
ALTER TABLE `achievements`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `roles`
--
ALTER TABLE `roles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `userstats`
--
ALTER TABLE `userstats`
  MODIFY `user_stat_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `user_achievements`
--
ALTER TABLE `user_achievements`
  MODIFY `achievement_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT a táblához `user_achievement_details`
--
ALTER TABLE `user_achievement_details`
  MODIFY `achievement_detail_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `User_fk0` FOREIGN KEY (`roleid`) REFERENCES `roles` (`id`),
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`user_stats_id`) REFERENCES `userstats` (`user_stat_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `user_achievements`
--
ALTER TABLE `user_achievements`
  ADD CONSTRAINT `user_achievements_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `user_achievement_details`
--
ALTER TABLE `user_achievement_details`
  ADD CONSTRAINT `user_achievement_details_ibfk_1` FOREIGN KEY (`user_achievement_id`) REFERENCES `user_achievements` (`achievement_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `user_achievement_details_ibfk_2` FOREIGN KEY (`achievement_id`) REFERENCES `achievements` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
