CREATE TABLE `User` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`username` char(32) NOT NULL UNIQUE,
	`email` char(64) NOT NULL UNIQUE,
	`regdate` DATETIME NOT NULL,
	`lastlogin` DATETIME NOT NULL,
	`permission_id` INT NOT NULL,
	`user_achievements_id` INT NOT NULL,
	`user_stats_id` INT NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `Permission` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`permission_name` char(32) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `UserAchievements` (
	`user_id` INT NOT NULL AUTO_INCREMENT,
	`achievement_id` INT NOT NULL,
	`achievement_date` DATETIME NOT NULL,
	PRIMARY KEY (`user_id`)
);

CREATE TABLE `UserStats` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `kills` INT NOT NULL,
  `deaths` INT NOT NULL,
  `timesplayed` INT NOT NULL,
  PRIMARY KEY (`user_id`)
);

CREATE TABLE `Achievements` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`achievement_name` char(255) NOT NULL,
	PRIMARY KEY (`id`)
);

ALTER TABLE `User` ADD CONSTRAINT `User_fk0` FOREIGN KEY (`permission_id`) REFERENCES `Permission`(`id`);

ALTER TABLE `UserAchievements` ADD CONSTRAINT `UserAchievements_fk0` FOREIGN KEY (`achievement_id`) REFERENCES `Achievements`(`id`);

ALTER TABLE `UserAchievements` ADD CONSTRAINT `UserAchievements_fk1` FOREIGN KEY (`user_id`) REFERENCES `User`(`id`) ON DELETE CASCADE ON UPDATE CASCADE;;

ALTER TABLE `UserStats` ADD CONSTRAINT `UserStats_fk0` FOREIGN KEY (`user_id`) REFERENCES `User`(`id`) ON DELETE CASCADE ON UPDATE CASCADE;
