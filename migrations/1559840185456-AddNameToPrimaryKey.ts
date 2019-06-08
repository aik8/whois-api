import { MigrationInterface, QueryRunner } from 'typeorm';

export class AddNameToPrimaryKey1559840185456 implements MigrationInterface {

	public async up(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_14fbedc1731d18b43baeabce2f0`');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP FOREIGN KEY `FK_f52393e74b799a7ece0342934a2`');
		await queryRunner.query('DROP INDEX `IDX_7d9896dcaeb646a4c60fa68dc1` ON `name_server`');
		await queryRunner.query('DROP INDEX `IDX_96d13dbf2e09f1f3454db520ff` ON `registrar`');
		await queryRunner.query('DROP INDEX `IDX_f52393e74b799a7ece0342934a` ON `snapshot_name_servers_name_server`');
		await queryRunner.query('ALTER TABLE `snapshot` ADD `registrarName` varchar(255) NULL');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD `nameServerName` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD PRIMARY KEY (`snapshotId`, `nameServerId`, `nameServerName`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `name_server` DROP COLUMN `name`');
		await queryRunner.query('ALTER TABLE `name_server` ADD `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `name` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `registrar` DROP COLUMN `name`');
		await queryRunner.query('ALTER TABLE `registrar` ADD `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `name` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `timestamp` `timestamp` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('CREATE INDEX `IDX_94e5023fd65630604a3b8715c7` ON `snapshot_name_servers_name_server` (`nameServerId`, `nameServerName`)');
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_6b9ac5b386fbe941c5422cce6f4` FOREIGN KEY (`registrarId`, `registrarName`) REFERENCES `registrar`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD CONSTRAINT `FK_94e5023fd65630604a3b8715c7c` FOREIGN KEY (`nameServerId`, `nameServerName`) REFERENCES `name_server`(`id`,`name`) ON DELETE CASCADE ON UPDATE NO ACTION');
	}

	public async down(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP FOREIGN KEY `FK_94e5023fd65630604a3b8715c7c`');
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_6b9ac5b386fbe941c5422cce6f4`');
		await queryRunner.query('DROP INDEX `IDX_94e5023fd65630604a3b8715c7` ON `snapshot_name_servers_name_server`');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `timestamp` `timestamp` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `name` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP COLUMN `name`');
		await queryRunner.query('ALTER TABLE `registrar` ADD `name` varchar(45) NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `name` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `name` varchar(255) NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP COLUMN `name`');
		await queryRunner.query('ALTER TABLE `name_server` ADD `name` varchar(45) NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`, `name`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `registrar` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `registrar` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL');
		await queryRunner.query('ALTER TABLE `name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `name_server` ADD PRIMARY KEY (`id`)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `id` `id` int NOT NULL AUTO_INCREMENT');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP PRIMARY KEY');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD PRIMARY KEY (`snapshotId`, `nameServerId`)');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP COLUMN `nameServerName`');
		await queryRunner.query('ALTER TABLE `snapshot` DROP COLUMN `registrarName`');
		await queryRunner.query('CREATE INDEX `IDX_f52393e74b799a7ece0342934a` ON `snapshot_name_servers_name_server` (`nameServerId`)');
		await queryRunner.query('CREATE UNIQUE INDEX `IDX_96d13dbf2e09f1f3454db520ff` ON `registrar` (`name`)');
		await queryRunner.query('CREATE UNIQUE INDEX `IDX_7d9896dcaeb646a4c60fa68dc1` ON `name_server` (`name`)');
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD CONSTRAINT `FK_f52393e74b799a7ece0342934a2` FOREIGN KEY (`nameServerId`) REFERENCES `name_server`(`id`) ON DELETE CASCADE ON UPDATE NO ACTION');
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_14fbedc1731d18b43baeabce2f0` FOREIGN KEY (`registrarId`) REFERENCES `registrar`(`id`) ON DELETE NO ACTION ON UPDATE NO ACTION');
	}

}
