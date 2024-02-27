-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 26. 11:01
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

--
-- A tábla adatainak kiíratása `blacklisted_tokens`
--

INSERT INTO `blacklisted_tokens` (`token_id`, `token`, `blacklisted_status_expires`) VALUES
('0b5c0ac8-8cdd-4864-ae6d-5bd7b6b568df', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTE4NCwiZXhwIjoxNzA4OTY3NTg0LCJpYXQiOjE3MDg4ODExODQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.M2ZdSwOgg4ScmyIZgP1IjAvdbncKUvBe0R3JOoPeyWw', '2024-02-26 17:13:04'),
('0ea47876-c8f7-4c5c-81ab-a6b2a1252d61', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg3OTk3NCwiZXhwIjoxNzA4OTY2Mzc0LCJpYXQiOjE3MDg4Nzk5NzQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.q2qUG5xzN0VawEn2HtXlhb1fA62ZapVgTwrcCz7FOO8', '2024-02-26 16:52:54'),
('57776836-1948-4eef-934e-a77037be8b84', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3MiwiZXhwIjoxNzA4OTY3ODcyLCJpYXQiOjE3MDg4ODE0NzIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.QfLQAB4LUdIQB2MKzcXZChIZ-x2IgfW4I7oKHZbM6oY', '2024-02-26 17:17:52'),
('60846ca4-58a4-4f25-8d1d-553e928a634c', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzMDg5NywiZXhwIjoxNzA5MDE3Mjk3LCJpYXQiOjE3MDg5MzA4OTcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.ptC8Bh1HA7SFAAH37kQM4qrOus2k1B3R1m6_PdOTKfM', '2024-02-27 07:01:37'),
('738c0e7c-5f2b-47e1-b909-d7180aa76483', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3NywiZXhwIjoxNzA4OTY3ODc3LCJpYXQiOjE3MDg4ODE0NzcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.H-UAV7kof_twmwGVJdy2WPjZVorkgNsbTUiqZsJeyCI', '2024-02-26 17:17:57'),
('7859c992-8ff6-499d-8e50-9b1c141735ce', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ4NSwiZXhwIjoxNzA4OTY3ODg1LCJpYXQiOjE3MDg4ODE0ODUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.p7f5wZx6PJluB1GSxhT4K1qL77gV74lkNsMTYX2sIzg', '2024-02-26 17:18:05'),
('bbd9c079-06fb-4707-abf6-e1c40eaae080', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MDA2NCwiZXhwIjoxNzA4OTY2NDY0LCJpYXQiOjE3MDg4ODAwNjQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.DC5_HzZNOq6x5g0YxKWcxYG3OKuy-mIplbnNrIFdEHg', '2024-02-26 16:54:24'),
('cc0abb64-ea43-446c-9029-f315155d6996', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzNDcxMCwiZXhwIjoxNzA5MDIxMTEwLCJpYXQiOjE3MDg5MzQ3MTAsImlzcyI6ImF1dGgtYXAiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.iHQfavfrFsZG5MnMCVbqztXQVdoRWNDufMeF5hTCnu4', '2024-02-27 08:05:10');

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
('56266d4c-2c05-4f6a-b3d9-55725b4593ac', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzNDg3MCwiZXhwIjoxNzA5MDIxMjcwLCJpYXQiOjE3MDg5MzQ4NzAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.P54kcnbfD9uQHZmaoZEixmEV9CxbNfXpmmki4pTUi4A');

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
('56266d4c-2c05-4f6a-b3d9-55725b4593ac', 'vitya0717', 'Tóth Viktor', 'tothv@kkszki.hu', '$2a$11$ik.XWqEYoSwCeWERwBBTDO8gUUt37BZiB9yL2O1YY5UvN9ThcSmjC', 1, '2024-02-22 14:02:06', 1, NULL);

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

--
-- A tábla adatainak kiíratása `registry`
--

INSERT INTO `registry` (`temp_userid`, `temp_username`, `temp_fullname`, `temp_email`, `temp_hash`, `temp_regdate`, `temp_roleid`, `temp_user_expire`, `temp_confirmation_key`) VALUES
('b091457f-2b16-488d-9c67-64cdf45a1e2e', 'xdddddd', 'senki', 'tviktor20000717@gmail.com', '$2a$11$d8QozU./fRwu5I61vRrb6upqOvMDLVT2rYORPQqopyLv8aES1ZRsi', '2024-02-26 07:39:25', 1, '2024-02-27 07:39:25', '$2a$04$NSuAsbXMtMqtslOaFAhM0.vHl8M6UmwcAT8mhpIbvAC./.uxFnvRe');

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
(2, 'User');

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
  MODIFY `roleid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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

CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTokens` ON SCHEDULE EVERY 1 DAY STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Clears out sessions table each hour.' DO DELETE FROM blacklisted_tokens WHERE blacklisted_tokens.blacklisted_status_expires < NOW()$$

DELIMITER ;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
