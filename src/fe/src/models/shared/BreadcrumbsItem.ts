export interface BreadcrumbsItem {
  disabled: boolean;
  exact: boolean;
  href: string;
  link: boolean;
  text: string | number;
  to: string | object;
}
