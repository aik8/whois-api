import { Table, Column, Model } from 'sequelize-typescript';

@Table({
	timestamps: true,
	tableName: 'registrars'
})
export class Registrar extends Model<Registrar> {
	@Column
	name: string;

	@Column
	url: string;

	@Column
	email: string;

	@Column
	phone: string;
}
