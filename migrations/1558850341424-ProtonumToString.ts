import { MigrationInterface, QueryRunner } from 'typeorm';

export class ProtonumToString1558850341424 implements MigrationInterface {

	public async up(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `timestamp` `timestamp` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `domain` DROP COLUMN `protonum`');
		await queryRunner.query('ALTER TABLE `domain` ADD `protonum` varchar(255) NULL');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)');
	}

	public async down(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `domain` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `domain` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `domain` DROP COLUMN `protonum`');
		await queryRunner.query('ALTER TABLE `domain` ADD `protonum` int NULL');
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `timestamp` `timestamp` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `updated_at` `updated_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
		await queryRunner.query('ALTER TABLE `name_server` CHANGE `created_at` `created_at` datetime(6) NOT NULL DEFAULT \'current_timestamp(6)\'');
	}

}
