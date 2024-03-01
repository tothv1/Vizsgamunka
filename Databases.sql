-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 29. 13:52
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
('067bd1d4-bc3c-4c15-856d-590db110fe58', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2ODgxNSwiZXhwIjoxNzA5MDU1MjE1LCJpYXQiOjE3MDg5Njg4MTUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.0ad1aq5oHDbzOEETxLb_wl_2vMW8HsamaMf3rc2GYPQ', '2024-02-27 17:33:35'),
('06f65c9d-d0d1-45a5-b3a1-3a4bb4c0d07a', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzNDg3MCwiZXhwIjoxNzA5MDIxMjcwLCJpYXQiOjE3MDg5MzQ4NzAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.P54kcnbfD9uQHZmaoZEixmEV9CxbNfXpmmki4pTUi4A', '2024-02-27 08:07:50'),
('0b5c0ac8-8cdd-4864-ae6d-5bd7b6b568df', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTE4NCwiZXhwIjoxNzA4OTY3NTg0LCJpYXQiOjE3MDg4ODExODQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.M2ZdSwOgg4ScmyIZgP1IjAvdbncKUvBe0R3JOoPeyWw', '2024-02-26 17:13:04'),
('0b5e5a19-a692-4813-8fac-9aeca8c157d0', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzA1OCwiZXhwIjoxNzA5MDUzNDU4LCJpYXQiOjE3MDg5NjcwNTgsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.a7pbzDmvxWPYRc0t5RhOonQv2bzjgKIVqE5cVXC0KMI', '2024-02-27 17:04:18'),
('0ea47876-c8f7-4c5c-81ab-a6b2a1252d61', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg3OTk3NCwiZXhwIjoxNzA4OTY2Mzc0LCJpYXQiOjE3MDg4Nzk5NzQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.q2qUG5xzN0VawEn2HtXlhb1fA62ZapVgTwrcCz7FOO8', '2024-02-26 16:52:54'),
('13c23926-19d1-4f31-bcb2-f31356cb22b4', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2Nzg0NiwiZXhwIjoxNzA5MDU0MjQ2LCJpYXQiOjE3MDg5Njc4NDYsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.LuCN0ubsjnPHxStHMXYn4dNC_Lk01SvoLINXTG8fNjI', '2024-02-27 17:17:26'),
('1656f932-9322-4539-920e-df433902ecfa', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2OTIwOSwiZXhwIjoxNzA5MDU1NjA5LCJpYXQiOjE3MDg5NjkyMDksImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.41YkpvzbeeFR078QFBvCVJURqjpQyqwtOKS2JDJN4WU', '2024-02-27 17:40:09'),
('19a2c60e-5eb1-4493-872c-93cac3285c25', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2Nzc2NywiZXhwIjoxNzA5MDU0MTY3LCJpYXQiOjE3MDg5Njc3NjcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.bWlGzZpabfhTjagLQevfTj9mqjcdHNtkJyCrBY6BNTk', '2024-02-27 17:16:07'),
('1b8dc524-6cab-4b64-9afa-e3d50d4c4d73', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzEzMSwiZXhwIjoxNzA5MDUzNTMxLCJpYXQiOjE3MDg5NjcxMzEsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.gd4jvVDgLFYa9Eh2E_7bbwJJ-55amtoGWqA3V3x0Xbg', '2024-02-27 17:05:31'),
('1d1daeab-5738-4d03-a4cc-e6aaf1bb76fd', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2Njg3MiwiZXhwIjoxNzA5MDUzMjcyLCJpYXQiOjE3MDg5NjY4NzIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.KiPWUc3GyVrauuWdA_ATcyFGwPIZ5MbjTDLphBYz0Fg', '2024-02-27 17:01:12'),
('20d71735-b8e0-4e8a-ac85-ca0902b6d0da', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2ODk0NywiZXhwIjoxNzA5MDU1MzQ3LCJpYXQiOjE3MDg5Njg5NDcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.4Eq5cuEI8n44gshh3hYgJXpDrAFrzzHBbC0zlnlTyxc', '2024-02-27 17:35:47'),
('216168e8-f978-47b3-8370-624b519ec576', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzQ2NiwiZXhwIjoxNzA5MDUzODY2LCJpYXQiOjE3MDg5Njc0NjYsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.c-iX35uOuyt_AIefAonKHIS4I8NkuBAHJxoUzEHlgiA', '2024-02-27 17:11:06'),
('26589033-d659-4096-a9e1-bc0c02b4c49b', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NTg3NiwiZXhwIjoxNzA5MDUyMjc2LCJpYXQiOjE3MDg5NjU4NzYsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.kxxGrHUkJ12AOHtivhdEH65r0HBh-KM4x3qzy13kZI4', '2024-02-27 16:44:36'),
('2aa3caf9-4b9d-45bd-a4f6-bcd3d86df495', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcwOTE0NDY5NCwiZXhwIjoxNzA5MjMxMDk0LCJpYXQiOjE3MDkxNDQ2OTQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.vKf1wbN-6oEB8NxpnLIqOi0enA8kv28vsUEd0aKU3cE', '2024-02-29 18:24:54'),
('2e99901e-00d7-480d-8b7f-1e03d38aa4f1', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcwOTIxMDYwMiwiZXhwIjoxNzA5Mjk3MDAyLCJpYXQiOjE3MDkyMTA2MDIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.LE3PyBDBSc_IyCps2Nr5jKKEMJx7OnFEfoz7ByrLHAg', '2024-03-01 12:43:22'),
('3120a960-dae7-48fc-8bc6-4d6397222b03', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzgyMiwiZXhwIjoxNzA5MDU0MjIyLCJpYXQiOjE3MDg5Njc4MjIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.7T-r6vOE2MvJBkqCoiHKcWidg6D4M28YNRRngwk7Xyo', '2024-02-27 17:17:02'),
('43956828-0049-4d9d-af93-577bcec3f716', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2OTExNCwiZXhwIjoxNzA5MDU1NTE0LCJpYXQiOjE3MDg5NjkxMTQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.B-ncH7sd8HUcEtzHkb6upvBtUFePv2yO3nI9xmbfSPA', '2024-02-27 17:38:34'),
('47221e44-2f1d-41d6-a746-1d970865b1e1', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NTk4MCwiZXhwIjoxNzA5MDUyMzgwLCJpYXQiOjE3MDg5NjU5ODAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ._tJHWrbNZaNi0U50rI0V02cFw9EmC-sT427cCZbcKwU', '2024-02-27 16:46:20'),
('4aa189ab-7bc8-4927-85ae-0d28461afc96', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcwOTE0OTExNiwiZXhwIjoxNzA5MjM1NTE2LCJpYXQiOjE3MDkxNDkxMTYsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.o8Z9DwLA_nQl1ajwGA8H3tr8IYrtTPtYMoRs9yLeW00', '2024-02-29 19:38:36'),
('4b588b07-61e6-45f0-9fb5-81eae630c348', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NjkwNywiZXhwIjoxNzA5MDUzMzA3LCJpYXQiOjE3MDg5NjY5MDcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.HGuHbvjECG0zwDRji0Lgi77-Lt1oRi6lfs-gA6rtM7U', '2024-02-27 17:01:47'),
('4f4c5c92-5a3f-4954-86b0-64469a9b735c', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzAwMCwiZXhwIjoxNzA5MDUzNDAwLCJpYXQiOjE3MDg5NjcwMDAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.bZSB-f3no8g5vHsdElqLvZ_1SNiTDSAIJh0GnnwvPpQ', '2024-02-27 17:03:20'),
('50c1c402-9bb8-4407-b34f-8c0f0861afbb', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NTk2NSwiZXhwIjoxNzA5MDUyMzY1LCJpYXQiOjE3MDg5NjU5NjUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.9fx9kVPl7mPX2kCeFQ97Et-L6RDgJ6KZggylziloMGU', '2024-02-27 16:46:05'),
('5130bc28-34f4-45bd-8d2b-ff479c9f1653', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2OTExNSwiZXhwIjoxNzA5MDU1NTE1LCJpYXQiOjE3MDg5NjkxMTUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.aAyDzzc9zr5cZF6hJfXOQtn5pduS5VYfotZSNOiroGA', '2024-02-27 17:38:35'),
('57776836-1948-4eef-934e-a77037be8b84', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3MiwiZXhwIjoxNzA4OTY3ODcyLCJpYXQiOjE3MDg4ODE0NzIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.QfLQAB4LUdIQB2MKzcXZChIZ-x2IgfW4I7oKHZbM6oY', '2024-02-26 17:17:52'),
('60846ca4-58a4-4f25-8d1d-553e928a634c', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzMDg5NywiZXhwIjoxNzA5MDE3Mjk3LCJpYXQiOjE3MDg5MzA4OTcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.ptC8Bh1HA7SFAAH37kQM4qrOus2k1B3R1m6_PdOTKfM', '2024-02-27 07:01:37'),
('67e41eb6-93ca-4958-8167-e4e09fb47636', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2ODkyMSwiZXhwIjoxNzA5MDU1MzIwLCJpYXQiOjE3MDg5Njg5MjEsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.t4H_dWW95qFHB1zr69j5PNlXNAD7TK6aA6gnWR9RJos', '2024-02-27 17:35:20'),
('738c0e7c-5f2b-47e1-b909-d7180aa76483', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ3NywiZXhwIjoxNzA4OTY3ODc3LCJpYXQiOjE3MDg4ODE0NzcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.H-UAV7kof_twmwGVJdy2WPjZVorkgNsbTUiqZsJeyCI', '2024-02-26 17:17:57'),
('7859c992-8ff6-499d-8e50-9b1c141735ce', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MTQ4NSwiZXhwIjoxNzA4OTY3ODg1LCJpYXQiOjE3MDg4ODE0ODUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.p7f5wZx6PJluB1GSxhT4K1qL77gV74lkNsMTYX2sIzg', '2024-02-26 17:18:05'),
('79c4a040-aaa3-4ab3-9989-d7c9ffedaf3e', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzczMiwiZXhwIjoxNzA5MDU0MTMyLCJpYXQiOjE3MDg5Njc3MzIsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.F3p6B0b_QS5hkFh-dLpgbVn_ezzkYLwvz7fmJGPr4co', '2024-02-27 17:15:32'),
('82f3b869-75ac-4c15-8965-c136a6c1c042', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzA3MywiZXhwIjoxNzA5MDUzNDczLCJpYXQiOjE3MDg5NjcwNzMsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.L2z-rWVN3o7ZbkNfUbvT8L03FBJ3gufLmgQRT5rR3yk', '2024-02-27 17:04:33'),
('96c81008-38cb-408a-8d1c-306bda7528c7', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzQ1NSwiZXhwIjoxNzA5MDUzODU1LCJpYXQiOjE3MDg5Njc0NTUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.ZHxtkH7EoTh7INnYuRhleIZTVxwb2pddzQi-q4-Hs1w', '2024-02-27 17:10:55'),
('9d111249-2c2f-4e11-ba8c-b2524faf2e21', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2OTA5NywiZXhwIjoxNzA5MDU1NDk3LCJpYXQiOjE3MDg5NjkwOTcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.Wyfto5MVAbcto5lM601M-W-Cf0dNJsgEF3SiMzLd9pE', '2024-02-27 17:38:17'),
('a139efb6-1573-49de-914f-f93b09df2e22', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcwOTE0MTc0OCwiZXhwIjoxNzA5MjI4MTQ4LCJpYXQiOjE3MDkxNDE3NDgsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.PKTNYmaZq073Eal6hwQ79PTtn1lu8yLwqG8TkXbz88s', '2024-02-29 17:35:48'),
('a409bd34-cce1-419b-900e-01bf42e58373', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NTcyNywiZXhwIjoxNzA5MDUyMTI3LCJpYXQiOjE3MDg5NjU3MjcsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.aFGbeQ1OqBzWAhk9Fap1-XGXQKItsHa559YgE_YflXA', '2024-02-27 16:42:07'),
('abd54eac-fbbf-4bf1-a713-7711b2f9237d', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NTc3MCwiZXhwIjoxNzA5MDUyMTcwLCJpYXQiOjE3MDg5NjU3NzAsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.OY6XpViQ2uZCQGxWtSCnjgyWw6Qc83p4XRn5eJYC7uc', '2024-02-27 16:42:50'),
('bbd9c079-06fb-4707-abf6-e1c40eaae080', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODg4MDA2NCwiZXhwIjoxNzA4OTY2NDY0LCJpYXQiOjE3MDg4ODAwNjQsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.DC5_HzZNOq6x5g0YxKWcxYG3OKuy-mIplbnNrIFdEHg', '2024-02-26 16:54:24'),
('bea07ff5-8b2a-4df7-9e88-05c6324559ed', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJVc2VyIiwidXNlclJlZ2RhdGUiOiIwMi8yNy8yMDI0IDE3OjA1OjU4IiwibmJmIjoxNzA5MTQxNjE4LCJleHAiOjE3MDkyMjgwMTgsImlhdCI6MTcwOTE0MTYxOCwiaXNzIjoiYXV0aC1hcGkiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.xLnroyj4FP6JSYpfq4d0tFKjTtr9hDO84NooBvcbdDs', '2024-02-29 17:33:38'),
('cb7849b8-b417-4673-94a1-fb636afd38ca', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2Nzc4NSwiZXhwIjoxNzA5MDU0MTg1LCJpYXQiOjE3MDg5Njc3ODUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.E1rGbuQhr6FqNUc51QNCvZSj9zbEeTOU0mewZQ5t8uU', '2024-02-27 17:16:25'),
('cc0abb64-ea43-446c-9029-f315155d6996', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODkzNDcxMCwiZXhwIjoxNzA5MDIxMTEwLCJpYXQiOjE3MDg5MzQ3MTAsImlzcyI6ImF1dGgtYXAiLCJhdWQiOiJhdXRoLWNsaWVudCJ9.iHQfavfrFsZG5MnMCVbqztXQVdoRWNDufMeF5hTCnu4', '2024-02-27 08:05:10'),
('eaaff462-9758-477f-86bf-e626bbe4dea9', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NjAwNSwiZXhwIjoxNzA5MDUyNDA1LCJpYXQiOjE3MDg5NjYwMDUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.hJsJny9MAHdfzF5DDUEtRfhHgslsAPExa65s2wixQvk', '2024-02-27 16:46:45'),
('fe3acf1a-6676-4753-9aa0-a0a2292e4ac1', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI1NjI2NmQ0Yy0yYzA1LTRmNmEtYjNkOS01NTcyNWI0NTkzYWMiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjIvMjAyNCAxNDowMjowNiIsIm5iZiI6MTcwODk2NzEyNSwiZXhwIjoxNzA5MDUzNTI1LCJpYXQiOjE3MDg5NjcxMjUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.i9fxgDcvLm4Y4sYmRgQObSvH_TCd2AyWDs4cdvFjMWU', '2024-02-27 17:05:25'),
('feb88e16-2da0-48ef-ade2-e373f8c1a351', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcwOTIwMDU3OCwiZXhwIjoxNzA5Mjg2OTc4LCJpYXQiOjE3MDkyMDA1NzgsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.YOGZnwlw-VJtPfFZFIfcj7VmxIwYC6VOSvkvPzWQ2_o', '2024-03-01 09:56:18');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `logged_in_users`
--

CREATE TABLE `logged_in_users` (
  `userid` varchar(254) NOT NULL,
  `token` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'senki', 'tothv@kkszki.hu', '$2a$11$O8K/izNpTKxIFIudTNPNZe0LOyoTpsC13dEXjeut6CZSazQJyRt3W', 0, '2024-02-27 17:05:58', 1, NULL);

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

DELIMITER $$
--
-- Események
--
CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTempUsers` ON SCHEDULE EVERY 1 HOUR STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Delete expired temp users' DO DELETE FROM registry WHERE registry.temp_user_expire < NOW()$$

CREATE DEFINER=`root`@`localhost` EVENT `deleteExpiredTokens` ON SCHEDULE EVERY 1 DAY STARTS '2024-02-19 08:42:52' ON COMPLETION NOT PRESERVE ENABLE COMMENT 'Clears out sessions table each hour.' DO DELETE FROM blacklisted_tokens WHERE blacklisted_tokens.blacklisted_status_expires < NOW()$$

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
(2, 'User');

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
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'tothv@kkszki.hu', '2024-02-27 18:06:08', '2024-02-29 13:43:22', 1, 2);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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
