import { Contact } from './vehicle';

export interface Vehicle {
  id: number;
  make: KeyValuePair;
  model: KeyValuePair;
  isRegistered: boolean;
  contact: Contact;
  features: KeyValuePair[];
  lastUpdate: string;
}

export interface SaveVehicle {
  id: number;
  modelId: number;
  makeId: number;
  isRegistered: boolean;
  contact: Contact;
  features: KeyValuePair[];
}

export interface Contact {
  name: string;
  email: string;
  phone: string;
}

export interface KeyValuePair {
    id: number;
    name: string;
}