export interface UserProfile {
  id: string;
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  profilePicture?: string;
  profilePictureBase64?: string;
}
