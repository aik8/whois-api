import { Column, CreateDateColumn, Entity, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';

@Entity()
export class NameServer {
	@PrimaryGeneratedColumn()
	id: number;

	@Column('varchar', { length: 45, unique: true })
	name: string;

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;
}
