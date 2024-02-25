-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 25. 19:28
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

--
-- A tábla adatainak kiíratása `blacklisted_tokens`
--

INSERT INTO `blacklisted_tokens` (`token_id`, `token`, `blacklisted_status_expires`) VALUES
('0749b6de-20db-4875-82b7-ac5f15a60252', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInVzZXJSb2xlIjoiQWRtaW4iLCJ1c2VyUmVnZGF0ZSI6IjAyLzIyLzIwMjQgMTQ6MDI6MDYiLCJuYmYiOjE3MDg2MTQxMzUsImV4cCI6MTcwODcwMDUzNSwiaWF0IjoxNzA4NjE0MTM1LCJpc3MiOiJhdXRoLWFwaSIsImF1ZCI6ImF1dGgtY2xpZW50In0.qgyq7-hjK5Tf1nfojxpErbTAkUW_Jyy64Fa0bxiKFK8', '2024-02-23 15:02:15'),
('0b5c0ac8-8cdd-4864-ae6d-5bd7b6b568df', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTE4NCwiZXhwIjoxNzA4OTY3NTg0LCJpYXQiOjE3MDg4ODExODQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.M2ZdSwOgg4ScmyIZgP1IjAvdbncKUvBe0R3JOoPeyWw', '2024-02-26 17:13:04'),
('0ea47876-c8f7-4c5c-81ab-a6b2a1252d61', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg3OTk3NCwiZXhwIjoxNzA4OTY2Mzc0LCJpYXQiOjE3MDg4Nzk5NzQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.q2qUG5xzN0VawEn2HtXlhb1fA62ZapVgTwrcCz7FOO8', '2024-02-26 16:52:54'),
('251ba48c-39f4-4242-bc25-4d16e0348153', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIwODc4MGY4MS1jNGQ4LTQ5OWYtOGI4Ni03ZGFiMjU3YWY0MzUiLCJ1c2VybmFtZSI6InZpdHlhMDcxOCIsInJvbGUiOiJVc2VyIiwidXNlclJlZ2RhdGUiOiIwMi8yMi8yMDI0IDE1OjMxOjIyIiwibmJmIjoxNzA4NjE1OTM5LCJleHAiOjE3MDg3MDIzMzksImlhdCI6MTcwODYxNTkzOSwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.b840tP8J1m-xuj-S7Isr3uA1IE9oyoxPPx6Rz6GBLes', '2024-02-23 15:32:19'),
('2b1ac0c2-01a9-40eb-a479-90edbc71363e', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxNjE4MCwiZXhwIjoxNzA4NzAyNTgwLCJpYXQiOjE3MDg2MTYxODAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.FxZYXAzQZnwwqGeS3kNPzgaiTcANb00RLiDSRMDeoVE', '2024-02-23 15:36:20'),
('419c2eee-7717-488e-9c18-60aaedb33775', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxNjQyMiwiZXhwIjoxNzA4NzAyODIyLCJpYXQiOjE3MDg2MTY0MjIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.VaCh4toJDKa3ulLXFiX0JzF04n18d1yrT5J97dqyQvo', '2024-02-23 15:40:22'),
('56e3c7f6-1433-46be-bd9c-a76536adcd69', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODc5ODg3MCwiZXhwIjoxNzA4ODg1MjcwLCJpYXQiOjE3MDg3OTg4NzAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.k_PT_zi9aKSGu9N6Qyfn2OKgqZnbt4cdp5NuslMZQE8', '2024-02-25 18:21:10'),
('57776836-1948-4eef-934e-a77037be8b84', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3MiwiZXhwIjoxNzA4OTY3ODcyLCJpYXQiOjE3MDg4ODE0NzIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.QfLQAB4LUdIQB2MKzcXZChIZ-x2IgfW4I7oKHZbM6oY', '2024-02-26 17:17:52'),
('738c0e7c-5f2b-47e1-b909-d7180aa76483', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3NywiZXhwIjoxNzA4OTY3ODc3LCJpYXQiOjE3MDg4ODE0NzcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.H-UAV7kof_twmwGVJdy2WPjZVorkgNsbTUiqZsJeyCI', '2024-02-26 17:17:57'),
('a1d2cf89-b5c6-4c97-b128-bc1d31a7143a', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInVzZXJSb2xlIjoiQWRtaW4iLCJ1c2VyUmVnZGF0ZSI6IjAyLzIyLzIwMjQgMTQ6MDI6MDYiLCJuYmYiOjE3MDg2MTUzMTksImV4cCI6MTcwODcwMTcxOSwiaWF0IjoxNzA4NjE1MzE5LCJpc3MiOiJhdXRoLWFwaSIsImF1ZCI6ImF1dGgtY2xpZW50In0.eJ2u2sTtLTB0Y6At3maOfUxmpqf4ktEZldm5FND3jZQ', '2024-02-23 15:21:59'),
('bbd9c079-06fb-4707-abf6-e1c40eaae080', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MDA2NCwiZXhwIjoxNzA4OTY2NDY0LCJpYXQiOjE3MDg4ODAwNjQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.DC5_HzZNOq6x5g0YxKWcxYG3OKuy-mIplbnNrIFdEHg', '2024-02-26 16:54:24'),
('c26ec648-2d38-4a43-9c21-226ada66f919', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInVzZXJSb2xlIjoiVXNlciIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxMzgwMSwiZXhwIjoxNzA4NzAwMjAxLCJpYXQiOjE3MDg2MTM4MDEsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.v1tjQaeZsNHW_OOfTO12hSTSJ5u6VVnBQBgXDFP04Y0', '2024-02-23 14:56:41'),
('d843133b-09f0-4c1f-b0f7-bb6e3af57c3c', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInVzZXJSb2xlIjoiQWRtaW4iLCJ1c2VyUmVnZGF0ZSI6IjAyLzIyLzIwMjQgMTQ6MDI6MDYiLCJuYmYiOjE3MDg2MTQ4ODQsImV4cCI6MTcwODcwMTI4NCwiaWF0IjoxNzA4NjE0ODg0LCJpc3MiOiJhdXRoLWFwaSIsImF1ZCI6ImF1dGgtY2xpZW50In0.c0_N8TDLrI3VkkzYL31edoP2kxkeJBy25PnPpBnRcOQ', '2024-02-23 15:14:44'),
('e171ad19-7f79-440c-b1e0-a1f292ee4f68', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxNTY5MiwiZXhwIjoxNzA4NzAyMDkyLCJpYXQiOjE3MDg2MTU2OTIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.8GZwNdG7NFY-H9IQQLWLwY8SynlWiCVEFtb3zOO_42U', '2024-02-23 15:28:12'),
('eadae021-eb82-47e1-aa80-86aa89857445', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxNjY0OSwiZXhwIjoxNzA4NzAzMDQ5LCJpYXQiOjE3MDg2MTY2NDksImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.z9QLCgOHwMmZ6Hj7Dq2gdRQAvwxT2Xzx2KAOt2nsaHE', '2024-02-23 15:44:09'),
('efc3d5a6-8b8a-435d-8e90-37b087e4057e', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxOTY1NywiZXhwIjoxNzA4NzA2MDU3LCJpYXQiOjE3MDg2MTk2NTcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.-ehmHJbaSM_ieDgg1sac-im_zGXi-SgR53sFIjZst3k', '2024-02-23 16:34:17'),
('f245bb97-2266-4ff6-bb26-568543c35ee2', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInVzZXJSb2xlIjoiVXNlciIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODYxMDU4NiwiZXhwIjoxNzA4Njk2OTg2LCJpYXQiOjE3MDg2MTA1ODYsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.QJ1txB6uV95QSei7BF29YVnWIC5Mnr20IGeq-OMDJPM', '2024-02-23 14:03:06');

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
('56266d4c-2c05-4f6a-b3d9-55725b4593ac', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ4NSwiZXhwIjoxNzA4OTY3ODg1LCJpYXQiOjE3MDg4ODE0ODUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.p7f5wZx6PJluB1GSxhT4K1qL77gV74lkNsMTYX2sIzg');

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
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
