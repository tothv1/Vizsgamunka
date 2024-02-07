CREATE TABLE `User` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`username` char(32) NOT NULL UNIQUE,
	`email` char(64) NOT NULL UNIQUE,
	`password` char(255) NOT NULL,
	`regdate` DATETIME NOT NULL,
	`lastlogin` DATETIME NOT NULL,
	`permission_id` INT NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `Permission` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`permission_name` char(32) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE `UserAchievements` (
	`achievement_id` INT NOT NULL,
	`user_id` INT NOT NULL,
	`achievement_date` DATETIME NOT NULL
);

CREATE TABLE `Achievements` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`achievement_name` char(255) NOT NULL,
	PRIMARY KEY (`id`)
);

ALTER TABLE `User` ADD CONSTRAINT `User_fk0` FOREIGN KEY (`permission_id`) REFERENCES `Permission`(`id`);

ALTER TABLE `UserAchievements` ADD CONSTRAINT `UserAchievements_fk0` FOREIGN KEY (`achievement_id`) REFERENCES `Achievements`(`id`);

ALTER TABLE `UserAchievements` ADD CONSTRAINT `UserAchievements_fk1` FOREIGN KEY (`user_id`) REFERENCES `User`(`id`);





