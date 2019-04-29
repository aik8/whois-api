import { Column, Model, Table } from 'sequelize-typescript';

@Table({
	timestamps: true
})
export class Domain extends Model<Domain> {
	@Column
	name: string;

	@Column
	handle: string;

	@Column
	creation: Date;

	@Column
	expiration: Date;

	@Column
	last_update: Date;
}
