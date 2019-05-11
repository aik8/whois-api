import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, UpdateDateColumn, VersionColumn, OneToMany } from 'typeorm';
import { Snapshot } from './snapshot.entity';

@Entity()
export class Registrar {
	@PrimaryGeneratedColumn()
	id: number;

	@Column('varchar', { length: 45 })
	name: string;

	@Column()
	url: string;

	@Column()
	email: string;

	@Column()
	phone: string;

	@OneToMany(type => Snapshot, snapshot => snapshot.registrar)
	snapshots: Snapshot[];

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;
}
