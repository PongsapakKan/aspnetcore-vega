import { Contact } from './contact';
import { KeyValuePair } from './key-value-pair';

export interface Vehicle {
  id: number;
  make: KeyValuePair;
  model: KeyValuePair;
  isRegistered: boolean;
  contact: Contact;
  features: KeyValuePair[];
  lastUpdate: string;
}