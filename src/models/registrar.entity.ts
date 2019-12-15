import { Column, CreateDateColumn, Entity, OneToMany, PrimaryColumn, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { IRegistrarCreateDto } from '../interfaces';
import { Snapshot } from './snapshot.entity';

@Entity()
export class Registrar {
	@PrimaryGeneratedColumn()
	id: number;

	@PrimaryColumn()
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
