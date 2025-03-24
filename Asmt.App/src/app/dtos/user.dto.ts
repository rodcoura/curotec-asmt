/**
 * User DTO.
 */
export interface UserDto {
    /**
     * The user ID.
     */
    id: number;

    /**
     * The user name.
     */
    name: string;

    /**
     * The user email.
     */
    email: string;

    /**
     * The user role.
     */
    role: string;

    /**
     * The user token.
     */
    token?: string;
}
