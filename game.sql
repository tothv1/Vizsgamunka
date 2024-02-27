-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 27. 18:08
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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `achievements_connect`
--

CREATE TABLE `achievements_connect` (
  `achi_id` int(11) NOT NULL,
  `userid` varchar(254) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

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
('bb0d5d3d-95ed-4243-a836-a0ed0e54d032', 'vitya0717', 'tothv@kkszki.hu', '2024-02-27 18:06:08', '0001-01-01 00:00:00', 2, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userachievements`
--

CREATE TABLE `userachievements` (
  `user_id` varchar(254) NOT NULL,
  `achievement_id` int(11) NOT NULL,
  `achi_connect_id` int(11) NOT NULL,
  `achievement_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

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
-- Tábla szerkezet ehhez a táblához `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240227164821_test', '8.0.1');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `achievements`
--
ALTER TABLE `achievements`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `achievements_connect`
--
ALTER TABLE `achievements_connect`
  ADD PRIMARY KEY (`achi_id`),
  ADD KEY `userid` (`userid`);

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
-- A tábla indexei `userachievements`
--
ALTER TABLE `userachievements`
  ADD PRIMARY KEY (`user_id`),
  ADD KEY `UserAchievements_fk0` (`achievement_id`),
  ADD KEY `achi_connect_id` (`achi_connect_id`);

--
-- A tábla indexei `userstats`
--
ALTER TABLE `userstats`
  ADD PRIMARY KEY (`user_stat_id`);

--
-- A tábla indexei `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `achievements`
--
ALTER TABLE `achievements`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `achievements_connect`
--
ALTER TABLE `achievements_connect`
  MODIFY `achi_id` int(11) NOT NULL AUTO_INCREMENT;

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
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `achievements_connect`
--
ALTER TABLE `achievements_connect`
  ADD CONSTRAINT `achievements_connect_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `User_fk0` FOREIGN KEY (`roleid`) REFERENCES `roles` (`id`),
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`user_stats_id`) REFERENCES `userstats` (`user_stat_id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `userachievements`
--
ALTER TABLE `userachievements`
  ADD CONSTRAINT `userachievements_ibfk_1` FOREIGN KEY (`achi_connect_id`) REFERENCES `achievements_connect` (`achi_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `userachievements_ibfk_2` FOREIGN KEY (`achievement_id`) REFERENCES `achievements` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
