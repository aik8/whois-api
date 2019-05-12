import { Column, CreateDateColumn, Entity, OneToMany, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { Snapshot } from './snapshot.entity';


@Entity()
export class Registrar {
	@PrimaryGeneratedColumn()
	id: number;

	@Column('varchar', { length: 45, unique: true })
	name: string;

	@Column({ nullable: true })
	url: string;

	@Column({ nullable: true })
	email: string;

	@Column({ nullable: true })
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
