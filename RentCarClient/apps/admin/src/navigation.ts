export interface NavigationModel{
    title: string; 
    url?: string;
    icon?: string;
    haveSubNav?: boolean;
    subNavs?: NavigationModel[]
}

export const navigations: NavigationModel[] = [
    {
        title: "Dashboard3",
        url: "/",
        icon: "bi-speedometer2"
    },
    {
        title: "Test",
        icon: "bi-users",
        haveSubNav: true,
        subNavs: [
            {
                title: "Test Sub Menu",
                url: "/test"
            },
        ]
    },

]