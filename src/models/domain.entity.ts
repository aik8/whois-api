import { Column, CreateDateColumn, Entity, OneToMany, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { Snapshot } from './snapshot.entity';


@Entity()
export class Domain {
	@PrimaryGeneratedColumn()
	id: number;

	@Column()
	name: string;

	@Column()
	handle: string;

	@Column()
	creation: Date;

	@Column()
	expiration: Date;

	@Column()
	last_update: Date;

	@OneToMany(type => Snapshot, snapshot => snapshot.domain)
	snapshots: Snapshot[];

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;
}
