-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Már 17. 19:38
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
CREATE DATABASE IF NOT EXISTS `auth` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `auth`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `blacklisted_tokens`
--

CREATE TABLE `blacklisted_tokens` (
  `token_id` varchar(254) NOT NULL,
  `token` text NOT NULL,
  `blacklisted_status_expires` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `logged_in_users`
--

CREATE TABLE `logged_in_users` (
  `userid` varchar(254) NOT NULL,
  `token` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `logged_in_users`
--

INSERT INTO `logged_in_users` (`userid`, `token`) VALUES
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDY5NTg0NSwiZXhwIjoxNzEwNzgyMjQ1LCJpYXQiOjE3MTA2OTU4NDUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.vNBSDgoue2p_taOHgYilMbmR1lDl891Cz-xSn55gDGo');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registered_users`
--

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
('08780f81-c4d8-499f-8b86-7dab257af435', 'vitya0718', 'Tóth Viktor', 'tothv2@kkszki.hu', '$2a$11$KGT43yK87kp17lq/bslZuOBDmpg.LDVFj0p6zFkBA0sgLMZPB2aaq', 0, '2024-02-22 15:31:22', 2, NULL),
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'senki', 'tothv@kkszki.hu', '$2a$11$O8K/izNpTKxIFIudTNPNZe0LOyoTpsC13dEXjeut6CZSazQJyRt3W', 1, '2024-02-27 17:05:58', 4, NULL);

--
-- Eseményindítók `registered_users`
--
DELIMITER $$
CREATE TRIGGER `deleteUserFromLoggedIn` AFTER DELETE ON `registered_users` FOR EACH ROW DELETE FROM logged_in_users WHERE logged_in_users.userid = OLD.userid
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `registry`
--

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
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTempUsers` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Delete expired temp users' DO DELETE FROM registry WHERE registry.temp_user_expire < NOW()$$

CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTokens` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Clears out sessions table each hour.' DO DELETE FROM blacklisted_tokens WHERE blacklisted_tokens.blacklisted_status_expires < NOW()$$

DELIMITER ;
--
-- Adatbázis: `game`
--
CREATE DATABASE IF NOT EXISTS `game` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `game`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `achievements`
--

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
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'tothv@kkszki.hu', '2024-02-27 18:06:08', '2024-03-17 18:17:25', 4, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userstats`
--

CREATE TABLE `userstats` (
  `user_stat_id` int(11) NOT NULL,
  `kills` int(11) NOT NULL,
  `deaths` int(11) NOT NULL,
  `timesplayed` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `userstats`
--

INSERT INTO `userstats` (`user_stat_id`, `kills`, `deaths`, `timesplayed`) VALUES
(2, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_achievements`
--

CREATE TABLE `user_achievements` (
  `achievement_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_achievements`
--

INSERT INTO `user_achievements` (`achievement_id`, `userid`) VALUES
(11, 'bb0d5d3d-95ed-4243-a836-a0ed0e54d032'),
(12, 'bb0d5d3d-95ed-4243-a836-a0ed0e54d032');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_achievement_details`
--

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
(8, 5, 12, '2024-02-28 20:16:39');

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
  MODIFY `user_stat_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `user_achievements`
--
ALTER TABLE `user_achievements`
  MODIFY `achievement_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT a táblához `user_achievement_details`
--
ALTER TABLE `user_achievement_details`
  MODIFY `achievement_detail_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `User_fk0` FOREIGN KEY (`roleid`) REFERENCES `roles` (`id`),
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`user_stats_id`) REFERENCES `userstats` (`user_stat_id`) ON DELETE CASCADE;

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
