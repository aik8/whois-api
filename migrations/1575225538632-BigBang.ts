import { MigrationInterface, QueryRunner } from 'typeorm';

export class BigBang1575225538632 implements MigrationInterface {
	name = 'BigBang1575225538632';

	public async up(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('CREATE TABLE `name_server` (`id` int NOT NULL AUTO_INCREMENT, `name` varchar(255) NOT NULL, `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `version` int NOT NULL, PRIMARY KEY (`id`, `name`)) ENGINE=InnoDB', undefined);
		await queryRunner.query('CREATE TABLE `registrar` (`id` int NOT NULL AUTO_INCREMENT, `name` varchar(255) NOT NULL, `url` varchar(255) NULL, `email` varchar(255) NULL, `phone` varchar(255) NULL, `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `version` int NOT NULL, PRIMARY KEY (`id`, `name`)) ENGINE=InnoDB', undefined);
		await queryRunner.query('CREATE TABLE `snapshot` (`id` int NOT NULL AUTO_INCREMENT, `timestamp` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `registered` tinyint NOT NULL DEFAULT 0, `registrarId` int NULL, `registrarName` varchar(255) NULL, `domainId` int NULL, `domainName` varchar(255) NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB', undefined);
		await queryRunner.query('CREATE TABLE `domain` (`id` int NOT NULL AUTO_INCREMENT, `name` varchar(255) NOT NULL, `handle` varchar(255) NULL, `protonum` varchar(255) NULL, `creation` datetime NULL, `expiration` datetime NULL, `last_update` datetime NULL, `registered` tinyint NOT NULL DEFAULT 0, `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6), `version` int NOT NULL, UNIQUE INDEX `IDX_45f55c9f40c31039afaaa90357` (`handle`), PRIMARY KEY (`id`, `name`)) ENGINE=InnoDB', undefined);
		await queryRunner.query('CREATE TABLE `snapshot_name_servers_name_server` (`snapshotId` int NOT NULL, `nameServerId` int NOT NULL, `nameServerName` varchar(255) NOT NULL, INDEX `IDX_890970bb13e0d8978aff311f27` (`snapshotId`), INDEX `IDX_94e5023fd65630604a3b8715c7` (`nameServerId`, `nameServerName`), PRIMARY KEY (`snapshotId`, `nameServerId`, `nameServerName`)) ENGINE=InnoDB', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_6b9ac5b386fbe941c5422cce6f4` FOREIGN KEY (`registrarId`, `registrarName`) REFERENCES `registrar`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_43f09af0274abf14e2a3b0d5103` FOREIGN KEY (`domainId`, `domainName`) REFERENCES `domain`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD CONSTRAINT `FK_890970bb13e0d8978aff311f27b` FOREIGN KEY (`snapshotId`) REFERENCES `snapshot`(`id`) ON DELETE CASCADE ON UPDATE NO ACTION', undefined);
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` ADD CONSTRAINT `FK_94e5023fd65630604a3b8715c7c` FOREIGN KEY (`nameServerId`, `nameServerName`) REFERENCES `name_server`(`id`,`name`) ON DELETE CASCADE ON UPDATE NO ACTION', undefined);
	}

	public async down(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP FOREIGN KEY `FK_94e5023fd65630604a3b8715c7c`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot_name_servers_name_server` DROP FOREIGN KEY `FK_890970bb13e0d8978aff311f27b`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_43f09af0274abf14e2a3b0d5103`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_6b9ac5b386fbe941c5422cce6f4`', undefined);
		await queryRunner.query('DROP INDEX `IDX_94e5023fd65630604a3b8715c7` ON `snapshot_name_servers_name_server`', undefined);
		await queryRunner.query('DROP INDEX `IDX_890970bb13e0d8978aff311f27` ON `snapshot_name_servers_name_server`', undefined);
		await queryRunner.query('DROP TABLE `snapshot_name_servers_name_server`', undefined);
		await queryRunner.query('DROP INDEX `IDX_45f55c9f40c31039afaaa90357` ON `domain`', undefined);
		await queryRunner.query('DROP TABLE `domain`', undefined);
		await queryRunner.query('DROP TABLE `snapshot`', undefined);
		await queryRunner.query('DROP TABLE `registrar`', undefined);
		await queryRunner.query('DROP TABLE `name_server`', undefined);
	}

}
