export interface MessageModel {
	Id: number;
	Text: string;
	CreatedDateTime: string;
	ChatId: number;
	UserId: number;
}

export interface MembersModel {
	[key: string]: string;
}

export interface MessagesModel {
	Members: MembersModel;
	Messages: MessageModel[];
}