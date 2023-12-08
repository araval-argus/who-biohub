import Vue from "vue";
import VueRouter, { RouteConfig, RouteMeta, RouterMode } from "vue-router";
import HomeView from "../views/HomeView.vue";
import AuthView from "../views/AuthView.vue";
import UnauthorizedView from "../views/UnauthorizedView.vue";
import ForbiddenView from "../views/ForbiddenView.vue";
import LaboratoriesPageDetails from "@/features/laboratories/LaboratoriesPageDetails.vue";
import LaboratoriesPageEdit from "@/features/laboratories/LaboratoriesPageEdit.vue";
import LaboratoriesPageCreate from "@/features/laboratories/LaboratoriesPageCreate.vue";
import LaboratoryInfoPage from "@/features/laboratories/LaboratoryInfoPage.vue";
import UserPageEdit from "@/features/users/UserPageEdit.vue";
import LaboratoriesUserPage from "@/features/laboratories/LaboratoriesUserPage.vue";
import LaboratoriesStaffPageIndex from "@/features/laboratories/LaboratoriesStaffPageIndex.vue";
import LaboratoriesStaffPageDetail from "@/features/laboratories/LaboratoriesStaffPageDetail.vue";
import LaboratoriesStaffPageEdit from "@/features/laboratories/LaboratoriesStaffPageEdit.vue";
import LaboratoriesStaffPageCreate from "@/features/laboratories/LaboratoriesStaffPageCreate.vue";

import LaboratoriesPendingStaffPageDetail from "@/features/laboratories/LaboratoriesPendingStaffPageDetail.vue";
import LaboratoriesPendingStaffPageEdit from "@/features/laboratories/LaboratoriesPendingStaffPageEdit.vue";

import BioHubFacilitiesPageDetails from "@/features/biohubfacilities/BioHubFacilitiesPageDetails.vue";
import BioHubFacilitiesPageEdit from "@/features/biohubfacilities/BioHubFacilitiesPageEdit.vue";
import BioHubFacilitiesPageCreate from "@/features/biohubfacilities/BioHubFacilitiesPageCreate.vue";
import BioHubFacilitiesPageIndex from "@/features/biohubfacilities/BioHubFacilitiesPageIndex.vue";
import BioHubFacilitiesUserPage from "@/features/biohubfacilities/BioHubFacilitiesUserPage.vue";
import BioHubFacilitiesStaffPageIndex from "@/features/biohubfacilities/BioHubFacilitiesStaffPageIndex.vue";
import BioHubFacilitiesStaffPageDetail from "@/features/biohubfacilities/BioHubFacilitiesStaffPageDetail.vue";
import BioHubFacilitiesStaffPageEdit from "@/features/biohubfacilities/BioHubFacilitiesStaffPageEdit.vue";
import BioHubFacilitiesStaffPageCreate from "@/features/biohubfacilities/BioHubFacilitiesStaffPageCreate.vue";

import BslLevelsPageDetails from "@/features/bsllevels/BslLevelsPageDetails.vue";
import BslLevelsPageEdit from "@/features/bsllevels/BslLevelsPageEdit.vue";
import BslLevelsPageCreate from "@/features/bsllevels/BslLevelsPageCreate.vue";
import BslLevelsPageIndex from "@/features/bsllevels/BslLevelsPageIndex.vue";

import CountriesPageDetails from "@/features/countries/CountriesPageDetails.vue";
import CountriesPageEdit from "@/features/countries/CountriesPageEdit.vue";
import CountriesPageCreate from "@/features/countries/CountriesPageCreate.vue";
import CountriesPageIndex from "@/features/countries/CountriesPageIndex.vue";

import MaterialsPageDetails from "@/features/materials/MaterialsPageDetails.vue";
import MaterialsPageDetailsPublic from "@/features/materials/MaterialsPageDetailsPublic.vue";
import MaterialsPageEdit from "@/features/materials/MaterialsPageEdit.vue";
import MaterialsPageCreate from "@/features/materials/MaterialsPageCreate.vue";
import MaterialsPageIndex from "@/features/materials/MaterialsPageIndex.vue";
import MaterialsLaboratoryPrivatePageIndex from "@/features/materials/MaterialsLaboratoryPrivatePageIndex.vue";

import MaterialsLaboratoryCompletionPageDetails from "@/features/materials/MaterialsLaboratoryCompletionPageDetails.vue";
import MaterialsBioHubFacilityCompletionPageDetails from "@/features/materials/MaterialsBioHubFacilityCompletionPageDetails.vue";

import MaterialsLaboratoryCompletionPageEdit from "@/features/materials/MaterialsLaboratoryCompletionPageEdit.vue";
import MaterialsBioHubFacilityCompletionPageEdit from "@/features/materials/MaterialsBioHubFacilityCompletionPageEdit.vue";

import ShipmentsPageIndex from "@/features/shipments/ShipmentsPageIndex.vue";
import ShipmentsPageDetails from "@/features/shipments/ShipmentsPageDetails.vue";

import ShipmentsPublicPageIndex from "@/features/shipments/ShipmentsPublicPageIndex.vue";
import ShipmentsPublicPageDetails from "@/features/shipments/ShipmentsPublicPageDetails.vue";

import UserRequestsPageCreatePublic from "@/features/userRequests/UserRequestsPageCreatePublic.vue";
import UserRequestsPageIndex from "@/features/userRequests/UserRequestsPageIndex.vue";
import UserRequestsPageApprove from "@/features/userRequests/UserRequestsPageApprove.vue";
import UserRequestsApprovedHistoryPageIndex from "@/features/userRequests/UserRequestsApprovedHistoryPageIndex.vue";
import UserRequestsPageApprovedDetail from "@/features/userRequests/UserRequestsPageApprovedDetail.vue";

import WhoUserPageDetails from "@/features/users/WhoUserPageDetails.vue";
import WhoUserPageEdit from "@/features/users/WhoUserPageEdit.vue";
import WhoUserPageCreate from "@/features/users/WhoUserPageCreate.vue";

import WhoPrivatePage from "@/views/WhoPrivatePage.vue";
import LaboratoryPrivatePage from "@/views/LaboratoryPrivatePage.vue";
import BioHubFacilityPrivatePage from "@/views/BioHubFacilityPrivatePage.vue";

import WorklistToBioHubItemsPageDetails from "@/features/worklistToBioHubItems/WorklistToBioHubItemsPageDetails.vue";
import WorklistFromBioHubItemsPageDetails from "@/features/worklistFromBioHubItems/WorklistFromBioHubItemsPageDetails.vue";

import ShipmentRequestsPageIndex from "@/features/shipmentRequests/ShipmentRequestsPageIndex.vue";
import ShipmentRequestsPageForm from "@/features/shipmentRequests/ShipmentRequestsPageForm.vue";

import CouriersPageDetails from "@/features/couriers/CouriersPageDetails.vue";
import CouriersPageEdit from "@/features/couriers/CouriersPageEdit.vue";
import CouriersPageCreate from "@/features/couriers/CouriersPageCreate.vue";
import CouriersPageIndex from "@/features/couriers/CouriersPageIndex.vue";
import CouriersUserPage from "@/features/couriers/CouriersUserPage.vue";
import CouriersStaffPageDetail from "@/features/couriers/CouriersStaffPageDetail.vue";
import CouriersStaffPageEdit from "@/features/couriers/CouriersStaffPageEdit.vue";
import CouriersStaffPageCreate from "@/features/couriers/CouriersStaffPageCreate.vue";

import CouriersBookingFormPageDetails from "@/features/couriers/CouriersBookingFormPageDetails.vue";

import DocumentsPageIndex from "@/features/documents/DocumentsPageIndex.vue";

import SMTA1WorkflowItemsPageDetails from "@/features/SMTA1WorkflowItems/SMTA1WorkflowItemsPageDetails.vue";
import SMTA2WorkflowItemsPageDetails from "@/features/SMTA2WorkflowItems/SMTA2WorkflowItemsPageDetails.vue";

import SMTARequestsPageIndex from "@/features/SMTARequests/SMTARequestsPageIndex.vue";
import SMTARequestsPageForm from "@/features/SMTARequests/SMTARequestsPageForm.vue";

import SMTAPastRequestsPageForm from "@/features/SMTARequests/SMTAPastRequestsPageForm.vue";
import ShipmentPastRequestsPageForm from "@/features/shipmentRequests/ShipmentPastRequestsPageForm.vue";

import ResourcesPageIndex from "@/features/resources/ResourcesPageIndex.vue";

import ResourcesPublicPageIndex from "@/features/resources/ResourcesPublicPageIndex.vue";

import EFormsPageIndex from "@/features/eforms/EFormsPageIndex.vue";

import Annex2OfSMTA1DataPage from "@/features/eforms/Annex2OfSMTA1DataPage.vue";

import BookingFormOfSMTA1DataPage from "@/features/eforms/BookingFormOfSMTA1DataPage.vue";

import Annex2OfSMTA2DataPage from "@/features/eforms/Annex2OfSMTA2DataPage.vue";

import BiosafetyChecklistOfSMTA2DataPage from "@/features/eforms/BiosafetyChecklistOfSMTA2DataPage.vue";

import BookingFormOfSMTA2DataPage from "@/features/eforms/BookingFormOfSMTA2DataPage.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },

  {
    path: "/auth",
    name: "auth",
    component: AuthView,
  },

  {
    path: "/unauthorized",
    name: "unauthorized",
    component: UnauthorizedView,
  },
  {
    path: "/forbidden",
    name: "forbidden",
    component: ForbiddenView,
  },

  {
    path: "/whoarea/dashboard",
    name: "whoarea",
    component: WhoPrivatePage,
  },

  {
    path: "/whoarea/laboratories",
    name: "whoarea-laboratories",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "laboratories" */ "../views/LaboratoriesView.vue"
      ),
  },
  {
    path: "/whoarea/laboratories/:id/details",
    name: "whoarea-laboratory-details",
    component: LaboratoriesPageDetails,
  },
  {
    path: "/whoarea/laboratories/:id/edit",
    name: "whoarea-laboratory-edit",
    component: LaboratoriesPageEdit,
  },
  {
    path: "/whoarea/laboratories/create",
    name: "whoarea-laboratory-create",
    component: LaboratoriesPageCreate,
  },

  {
    path: "/laboratoryarea/dashboard",
    name: "laboratoryarea",
    component: LaboratoryPrivatePage,
  },
  {
    path: "/laboratoryarea/user/edit",
    name: "laboratoryarea-user-edit",
    component: UserPageEdit,
  },
  {
    path: "/laboratoryarea/info",
    name: "laboratoryarea-info-page",
    component: LaboratoryInfoPage,
  },
  {
    path: "/laboratoryarea/edit",
    name: "laboratoryarea-private-page-edit",
    component: LaboratoriesPageEdit,
  },
  {
    path: "/whoarea/bioHubFacilities",
    name: "whoarea-bioHubFacilities",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "bioHubFacilities" */ "../views/BioHubFacilitiesView.vue"
      ),
  },
  {
    path: "/whoarea/biohubfacilities/:id/detail",
    name: "whoarea-biohubfacility-details",
    component: BioHubFacilitiesPageDetails,
  },
  {
    path: "/whoarea/bioHubFacilities/:id/edit",
    name: "whoarea-biohubfacility-edit",
    component: BioHubFacilitiesPageEdit,
  },
  {
    path: "/whoarea/bioHubFacilities/create",
    name: "whoarea-biohubfacility-create",
    component: BioHubFacilitiesPageCreate,
  },

  // For the moment it is hidden, maybe in the future it will be resumed
  // {
  //   path: "/whoarea/bslLevels",
  //   name: "whoarea-bslLevels",

  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () =>
  //     import(
  //       /* webpackChunkName: "bioHubFacilities" */ "../views/BslLevelsView.vue"
  //     ),
  // },
  // {
  //   path: "/whoarea/bsllevels/:id/detail",
  //   name: "whoarea-bsllevel-details",
  //   component: BslLevelsPageDetails,
  // },
  // {
  //   path: "/whoarea/bslLevels/:id/edit",
  //   name: "whoarea-bsllevel-edit",
  //   component: BslLevelsPageEdit,
  // },
  // {
  //   path: "/whoarea/bslLevels/create",
  //   name: "whoarea-bsllevel-create",
  //   component: BslLevelsPageCreate,
  // },

  // For the moment it is hidden, maybe in the future it will be resumed
  // {
  //   path: "/whoarea/countries",
  //   name: "whoarea-countries",

  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () =>
  //     import(
  //       /* webpackChunkName: "bioHubFacilities" */ "../views/CountriesView.vue"
  //     ),
  // },
  // {
  //   path: "/whoarea/countries/:id/detail",
  //   name: "whoarea-country-details",
  //   component: CountriesPageDetails,
  // },
  // {
  //   path: "/whoarea/countries/:id/edit",
  //   name: "whoarea-country-edit",
  //   component: CountriesPageEdit,
  // },
  // {
  //   path: "/whoarea/countries/create",
  //   name: "whoarea-country-create",
  //   component: CountriesPageCreate,
  // },

  {
    path: "/whoarea/bmepp",
    name: "whoarea-materials",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },
  {
    path: "/whoarea/bmepp/:id/detail",
    name: "whoarea-material-details-bmepp",
    component: MaterialsPageDetails,
  },
  {
    path: "/whoarea/bmepp/:id/edit",
    name: "whoarea-material-edit",
    component: MaterialsPageEdit,
  },

  {
    path: "/whoarea/bmepp/:id/details/laboratorycompletion",
    name: "whoarea-material-details-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageDetails,
  },

  {
    path: "/whoarea/bmepp/:id/details/biohubcompletion",
    name: "whoarea-material-details-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageDetails,
  },

  {
    path: "/laboratoryarea/bmepp/:id/details/laboratorycompletion",
    name: "laboratoryarea-material-details-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageDetails,
  },

  {
    path: "/laboratoryarea/bmepp/:id/details/biohubcompletion",
    name: "laboratoryarea-material-details-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageDetails,
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/details/laboratorycompletion",
    name: "biohubfacilityarea-material-details-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageDetails,
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/details/biohubcompletion",
    name: "biohubfacilityarea-material-details-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageDetails,
  },

  {
    path: "/whoarea/bmepp/:id/edit/laboratorycompletion",
    name: "whoarea-material-edit-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageEdit,
  },

  {
    path: "/whoarea/bmepp/:id/edit/biohubcompletion",
    name: "whoarea-material-edit-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageEdit,
  },

  {
    path: "/laboratoryarea/bmepp/:id/edit/laboratorycompletion",
    name: "laboratoryarea-material-edit-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageEdit,
  },

  {
    path: "/laboratoryarea/bmepp/:id/edit/biohubcompletion",
    name: "laboratoryarea-material-edit-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageEdit,
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/edit/laboratorycompletion",
    name: "biohubfacilityarea-material-edit-laboratorycompletion",
    component: MaterialsLaboratoryCompletionPageEdit,
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/edit/biohubcompletion",
    name: "biohubfacilityarea-material-edit-biohubcompletion",
    component: MaterialsBioHubFacilityCompletionPageEdit,
  },

  // {
  //   path: "/whoarea/bmepp/create",
  //   name: "whoarea-material-create",
  //   component: MaterialsPageCreate,
  // },

  {
    path: "/laboratoryarea/bmepp",
    name: "laboratoryarea-materials-bmepp",
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },

  {
    path: "/laboratoryarea/:id/bmepp/detail",
    name: "laboratoryarea-material-details-bmepp",
    component: MaterialsPageDetails,
  },

  {
    path: "/laboratoryarea/:id/bmepp/edit",
    name: "laboratoryarea-material-edit",
    component: MaterialsPageEdit,
  },
  // {
  //   path: "/laboratoryarea/bmepp/create",
  //   name: "laboratoryarea-material-create",
  //   component: MaterialsPageCreate,
  // },
  {
    path: "/laboratoryarea/bmeppcatalogue",
    name: "laboratoryarea-materials-bmepp-catalogue",
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },
  {
    path: "/laboratoryarea/bmeppcatalogue/:providerId/provider",
    name: "laboratoryarea-materials-bmepp-catalogue-provider",
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },
  {
    path: "/laboratoryarea/:id/bmeppcatalogue/detail",
    name: "laboratoryarea-material-details-bmepp-catalogue",
    component: MaterialsPageDetails,
  },

  {
    path: "/public/bmeppcatalogue",
    name: "public-materials",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/MaterialsViewPublic.vue"
      ),
  },

  {
    path: "/public/:id/bmeppcatalogue/detail",
    name: "public-material-details",
    component: MaterialsPageDetailsPublic,
  },

  {
    path: "/public/bmeppcatalogue/:providerId/provider",
    name: "public-materials-provider",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/MaterialsViewPublic.vue"
      ),
  },

  {
    path: "/public/:id/bmeppcatalogue/:providerId/provider/detail",
    name: "public-material-provider-details",
    component: MaterialsPageDetailsPublic,
  },

  {
    path: "/whoarea/shipments",
    name: "whoarea-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },

  {
    path: "/biohubfacilityarea/shipments",
    name: "biohubfacilityarea-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },
  {
    path: "/laboratoryarea/shipments",
    name: "laboratoryarea-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },

  {
    path: "/whoarea/shipments/:laboratoryId/laboratory",
    name: "whoarea-laboratory-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },

  {
    path: "/public/shipments/:laboratoryId/laboratory",
    name: "public-laboratory-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsPublicByLaboratoryView.vue"
      ),
  },

  {
    path: "/laboratoryarea/shipments/:laboratoryId/laboratory",
    name: "laboratoryarea-laboratory-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },

  {
    path: "/whoarea/:id/shipment/detail",
    name: "whoarea-shipment-details",
    component: ShipmentsPageDetails,
  },

  {
    path: "/laboratoryarea/:id/shipment/detail",
    name: "laboratoryarea-shipment-details",
    component: ShipmentsPageDetails,
  },

  {
    path: "/biohubfacilityarea/:id/shipment/detail",
    name: "biohubfacilityarea-shipment-details",
    component: ShipmentsPageDetails,
  },

  {
    path: "/public/:id/shipment/detail",
    name: "public-shipment-details",
    component: ShipmentsPublicPageDetails,
  },

  {
    path: "/whoarea/bmepp/:providerId/provider",
    name: "whoarea-materials-provider",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },

  {
    path: "/whoarea/:id/bmepp/:providerId/provider/detail",
    name: "whoarea-material-provider-details",
    component: MaterialsPageDetails,
  },

  {
    path: "/resources",
    name: "resources",
    component: ResourcesPublicPageIndex,
  },

  //TODO: For the moment it is commented, in the future it will be reintroduced
  // {
  //   path: "/faq",
  //   name: "faq",

  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () =>
  //     import(/* webpackChunkName: "faq" */ "../views/FaqView.vue"),
  // },

  {
    path: "/userrequest",
    name: "user-request",
    component: UserRequestsPageCreatePublic,
  },

  {
    path: "/whoarea/userrequests",
    name: "whoarea-user-requests",
    component: UserRequestsPageIndex,
  },

  {
    path: "/whoarea/userrequests/:id/approve",
    name: "whoarea-user-request-approve",
    component: UserRequestsPageApprove,
  },

  {
    path: "/whoarea/approveduserrequest/:id/detail",
    name: "whoarea-user-request-approved-detail",
    component: UserRequestsPageApprovedDetail,
  },

  {
    path: "/whoarea/approveduserrequests",
    name: "whoarea-approved-user-requests-history",
    component: UserRequestsApprovedHistoryPageIndex,
  },

  {
    path: "/laboratoryarea/profile",
    name: "laboratoryarea-profile",
    component: LaboratoriesUserPage,
  },
  {
    path: "/laboratoryarea/staff",
    name: "laboratoryarea-staff",
    component: LaboratoriesStaffPageIndex,
  },
  {
    path: "/laboratoryarea/staff/:id/detail",
    name: "laboratoryarea-staff-details",
    component: LaboratoriesStaffPageDetail,
  },
  {
    path: "/laboratoryarea/staff/create",
    name: "laboratoryarea-staff-create",
    component: LaboratoriesStaffPageCreate,
  },
  {
    path: "/laboratoryarea/staff/:id/edit",
    name: "laboratoryarea-staff-edit",
    component: LaboratoriesStaffPageEdit,
  },

  {
    path: "/whoarea/laboratory/staff/:id/detail",
    name: "whoarea-laboratory-staff-details",
    component: LaboratoriesStaffPageDetail,
  },
  {
    path: "/whoarea/laboratory/staff/:id/create",
    name: "whoarea-laboratory-staff-create",
    component: LaboratoriesStaffPageCreate,
  },
  {
    path: "/whoarea/laboratory/staff/:id/edit",
    name: "whoarea-laboratory-staff-edit",
    component: LaboratoriesStaffPageEdit,
  },

  {
    path: "/laboratoryarea/pendingstaff/:id/detail",
    name: "laboratoryarea-pending-staff-details",
    component: LaboratoriesPendingStaffPageDetail,
  },

  {
    path: "/laboratoryarea/pendingstaff/:id/edit",
    name: "laboratoryarea-pending-staff-edit",
    component: LaboratoriesPendingStaffPageEdit,
  },

  {
    path: "/laboratoryarea/templates",
    name: "laboratoryarea-template",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "document" */ "../views/LaboratoryDocumentView.vue"
      ),
  },

  {
    path: "/biohubfacilityarea/staff",
    name: "biohubfacilityarea-staff",
    component: BioHubFacilitiesStaffPageIndex,
  },
  {
    path: "/biohubfacilityarea/biohubfacility/staff/:id/detail",
    name: "biohubfacilityarea-biohubfacility-staff-details",
    component: BioHubFacilitiesStaffPageDetail,
  },
  {
    path: "/whoarea/biohubfacility/staff/:id/detail",
    name: "whoarea-biohubfacility-staff-details",
    component: BioHubFacilitiesStaffPageDetail,
  },
  {
    path: "/whoarea/biohubfacility/staff/:id/edit",
    name: "whoarea-biohubfacility-staff-edit",
    component: BioHubFacilitiesStaffPageEdit,
  },
  {
    path: "/whoarea/biohubfacility/:id/staff/create",
    name: "whoarea-biohubfacility-staff-create",
    component: BioHubFacilitiesStaffPageCreate,
  },

  {
    path: "/laboratoryarea/smtasubmission",
    name: "laboratoryarea-smta-submission",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "document" */ "../views/LaboratorySmtaSubmissionView.vue"
      ),
  },

  {
    path: "/laboratoryarea/shipmentrequest",
    name: "laboratoryarea-shipment-request",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "document" */ "../views/LaboratoryShipmentRequestView.vue"
      ),
  },

  {
    path: "/biohubfacilityarea/dashboard",
    name: "biohubfacilityarea",
    component: BioHubFacilityPrivatePage,
  },

  {
    path: "/biohubfacilityarea/bmepp",
    name: "biohubfacilityarea-materials-bmepp",
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/detail",
    name: "biohubfacilityarea-material-details-bmepp",
    component: MaterialsPageDetails,
  },

  {
    path: "/biohubfacilityarea/bmepp/:id/edit",
    name: "biohubfacilityarea-material-edit",
    component: MaterialsPageEdit,
  },

  // TODO: For the moment it is hidden, maybe in the future it will be resumed
  // {
  //   path: "/biohubfacilityarea/bmepp/create",
  //   name: "biohubfacilityarea-material-create",
  //   component: MaterialsPageCreate,
  // },

  {
    path: "/biohubfacilityarea/bmepp/:providerId/provider",
    name: "biohubfacilityarea-materials-bmepp-provider",
    component: () =>
      import(/* webpackChunkName: "materials" */ "../views/MaterialsView.vue"),
  },

  {
    path: "/biohubfacilityarea/shipments/:laboratoryId/laboratory",
    name: "biohubfacilityarea-biohubfacility-shipments",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "materials" */ "../views/ShipmentsByLaboratoryView.vue"
      ),
  },
  {
    path: "/biohubfacilityarea/profile",
    name: "biohubfacilityarea-profile",
    component: BioHubFacilitiesUserPage,
  },
  {
    path: "/biohubfacilityarea/user/edit",
    name: "biohubfacilityarea-user-edit",
    component: UserPageEdit,
  },
  {
    path: "/biohubfacilityarea/laboratories",
    name: "biohubfacilityarea-laboratories",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "laboratories" */ "../views/LaboratoriesView.vue"
      ),
  },
  {
    path: "/biohubfacilityarea/laboratories/:id/details",
    name: "biohubfacilityarea-laboratory-details",
    component: LaboratoriesPageDetails,
  },
  {
    path: "/biohubfacilityarea/templates",
    name: "biohubfacilityarea-template",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(
        /* webpackChunkName: "document" */ "../views/BioHubFacilityDocumentView.vue"
      ),
  },

  {
    path: "/whoarea/users",
    name: "whoarea-users",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "document" */ "../views/WhoUsersView.vue"),
  },
  {
    path: "/whoarea/users/:id/details",
    name: "whoarea-user-details",
    component: WhoUserPageDetails,
  },
  {
    path: "/whoarea/users/create",
    name: "whoarea-user-create",
    component: WhoUserPageCreate,
  },
  {
    path: "/whoarea/users/:id/edit",
    name: "whoarea-user-edit",
    component: WhoUserPageEdit,
  },

  {
    path: "/whoarea/templates",
    name: "whoarea-template",

    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "document" */ "../views/WhoDocumentView.vue"),
  },

  {
    path: "/whoarea/worklistToBioHubItems/:id/detail",
    name: "whoarea-worklist-to-bio-hub-details",
    component: WorklistToBioHubItemsPageDetails,
  },

  {
    path: "/laboratoryarea/worklistToBioHubItems/:id/detail",
    name: "laboratoryarea-worklist-to-bio-hub-details",
    component: WorklistToBioHubItemsPageDetails,
  },

  {
    path: "/biohubfacilityarea/worklistToBioHubItems/:id/detail",
    name: "biohubfacilityarea-worklist-to-bio-hub-details",
    component: WorklistToBioHubItemsPageDetails,
  },

  {
    path: "/whoarea/worklistFromBioHubItems/:id/detail",
    name: "whoarea-worklist-from-bio-hub-details",
    component: WorklistFromBioHubItemsPageDetails,
  },

  {
    path: "/laboratoryarea/worklistFromBioHubItems/:id/detail",
    name: "laboratoryarea-worklist-from-bio-hub-details",
    component: WorklistFromBioHubItemsPageDetails,
  },

  {
    path: "/biohubfacilityarea/worklistFromBioHubItems/:id/detail",
    name: "biohubfacilityarea-worklist-from-bio-hub-details",
    component: WorklistFromBioHubItemsPageDetails,
  },

  {
    path: "/whoarea/shipmentrequests",
    name: "whoarea-shipment-requests",
    component: ShipmentRequestsPageIndex,
  },

  {
    path: "/laboratoryarea/shipmentrequests",
    name: "laboratoryarea-shipment-requests",
    component: ShipmentRequestsPageIndex,
  },

  {
    path: "/biohubfacilityarea/shipmentrequests",
    name: "biohubfacilityarea-shipment-requests",
    component: ShipmentRequestsPageIndex,
  },

  {
    path: "/whoarea/shipmentrequestform",
    name: "whoarea-shipment-request-form",
    component: ShipmentRequestsPageForm,
  },

  {
    path: "/laboratoryarea/shipmentrequestform",
    name: "laboratoryarea-shipment-request-form",
    component: ShipmentRequestsPageForm,
  },

  {
    path: "/biohubfacilityarea/shipmentrequestform",
    name: "biohubfacilityarea-shipment-request-form",
    component: ShipmentRequestsPageForm,
  },

  {
    path: "/whoarea/couriers/:id/detail",
    name: "whoarea-courier-details",
    component: CouriersPageDetails,
  },
  {
    path: "/whoarea/couriers/:id/edit",
    name: "whoarea-courier-edit",
    component: CouriersPageEdit,
  },
  {
    path: "/whoarea/couriers/create",
    name: "whoarea-courier-create",
    component: CouriersPageCreate,
  },
  {
    path: "/whoarea/couriers",
    name: "whoarea-couriers",
    component: CouriersPageIndex,
  },

  {
    path: "/whoarea/courier/staff/:id/detail",
    name: "whoarea-courier-staff-details",
    component: CouriersStaffPageDetail,
  },
  {
    path: "/whoarea/courier/staff/:id/edit",
    name: "whoarea-courier-staff-edit",
    component: CouriersStaffPageEdit,
  },
  {
    path: "/whoarea/courier/:id/staff/create",
    name: "whoarea-courier-staff-create",
    component: CouriersStaffPageCreate,
  },
  {
    path: "/whoarea/courier/bookingform/:id/detail",
    name: "whoarea-courier-booking-form-info",
    component: CouriersBookingFormPageDetails,
  },

  {
    path: "/whoarea/documents",
    name: "whoarea-documents",
    component: DocumentsPageIndex,
  },

  {
    path: "/laboratoryarea/documents",
    name: "laboratoryarea-documents",
    component: DocumentsPageIndex,
  },

  {
    path: "/biohubfacilityarea/documents",
    name: "biohubfacilityarea-documents",
    component: DocumentsPageIndex,
  },

  {
    path: "/whoarea/eforms",
    name: "whoarea-eforms",
    component: EFormsPageIndex,
  },

  {
    path: "/laboratoryarea/eforms",
    name: "laboratoryarea-eforms",
    component: EFormsPageIndex,
  },

  {
    path: "/biohubfacilityarea/eforms",
    name: "biohubfacilityarea-eforms",
    component: EFormsPageIndex,
  },

  {
    path: "/whoarea/smta1workflowItems/:id/detail",
    name: "whoarea-smta1-workflow-details",
    component: SMTA1WorkflowItemsPageDetails,
  },

  {
    path: "/laboratoryarea/smta1workflowItems/:id/detail",
    name: "laboratoryarea-smta1-workflow-details",
    component: SMTA1WorkflowItemsPageDetails,
  },

  {
    path: "/whoarea/smta2workflowItems/:id/detail",
    name: "whoarea-smta2-workflow-details",
    component: SMTA2WorkflowItemsPageDetails,
  },

  {
    path: "/laboratoryarea/smta2workflowItems/:id/detail",
    name: "laboratoryarea-smta2-workflow-details",
    component: SMTA2WorkflowItemsPageDetails,
  },

  {
    path: "/whoarea/smtarequests",
    name: "whoarea-smta-requests",
    component: SMTARequestsPageIndex,
  },

  {
    path: "/laboratoryarea/smtarequests",
    name: "laboratoryarea-smta-requests",
    component: SMTARequestsPageIndex,
  },

  {
    path: "/whoarea/smtarequestform",
    name: "whoarea-smta-request-form",
    component: SMTARequestsPageForm,
  },

  {
    path: "/laboratoryarea/smtarequestform",
    name: "laboratoryarea-smta-request-form",
    component: SMTARequestsPageForm,
  },

  {
    path: "/laboratoryarea/smtapastrequestform",
    name: "laboratoryarea-smta-past-request-form",
    component: SMTAPastRequestsPageForm,
  },
  {
    path: "/laboratoryarea/shipmentpastrequestform",
    name: "laboratoryarea-shipment-past-request-form",
    component: ShipmentPastRequestsPageForm,
  },

  {
    path: "/whoarea/resources",
    name: "whoarea-resources",
    component: ResourcesPageIndex,
  },

  {
    path: "/whoarea/eforms/:id/annex2ofsmta1",
    name: "whoarea-eform-annex2ofsmta1",
    component: Annex2OfSMTA1DataPage,
  },

  {
    path: "/laboratoryarea/eforms/:id/annex2ofsmta1",
    name: "laboratoryarea-eform-annex2ofsmta1",
    component: Annex2OfSMTA1DataPage,
  },

  {
    path: "/biohubfacilityarea/eforms/:id/annex2ofsmta1",
    name: "biohubfacilityarea-eform-annex2ofsmta1",
    component: Annex2OfSMTA1DataPage,
  },

  {
    path: "/whoarea/eforms/:id/bookingformofsmta1",
    name: "whoarea-eform-bookingformofsmta1",
    component: BookingFormOfSMTA1DataPage,
  },

  {
    path: "/laboratoryarea/eforms/:id/bookingformofsmta1",
    name: "laboratoryarea-eform-bookingformofsmta1",
    component: BookingFormOfSMTA1DataPage,
  },

  {
    path: "/biohubfacilityarea/eforms/:id/bookingformofsmta1",
    name: "biohubfacilityarea-eform-bookingformofsmta1",
    component: BookingFormOfSMTA1DataPage,
  },

  {
    path: "/whoarea/eforms/:id/annex2ofsmta2",
    name: "whoarea-eform-annex2ofsmta2",
    component: Annex2OfSMTA2DataPage,
  },

  {
    path: "/laboratoryarea/eforms/:id/annex2ofsmta2",
    name: "laboratoryarea-eform-annex2ofsmta2",
    component: Annex2OfSMTA2DataPage,
  },

  {
    path: "/biohubfacilityarea/eforms/:id/annex2ofsmta2",
    name: "biohubfacilityarea-eform-annex2ofsmta2",
    component: Annex2OfSMTA2DataPage,
  },

  {
    path: "/whoarea/eforms/:id/biosafetychecklistofsmta2",
    name: "whoarea-eform-biosafetychecklistofsmta2",
    component: BiosafetyChecklistOfSMTA2DataPage,
  },

  {
    path: "/laboratoryarea/eforms/:id/biosafetychecklistofsmta2",
    name: "laboratoryarea-eform-biosafetychecklistofsmta2",
    component: BiosafetyChecklistOfSMTA2DataPage,
  },

  {
    path: "/biohubfacilityarea/eforms/:id/biosafetychecklistofsmta2",
    name: "biohubfacilityarea-eform-biosafetychecklistofsmta2",
    component: BiosafetyChecklistOfSMTA2DataPage,
  },

  {
    path: "/biohubfacilityarea/eforms/:id/bookingformofsmta2",
    name: "biohubfacilityarea-eform-bookingformofsmta2",
    component: BookingFormOfSMTA2DataPage,
  },

  {
    path: "/whoarea/eforms/:id/bookingformofsmta2",
    name: "whoarea-eform-bookingformofsmta2",
    component: BookingFormOfSMTA2DataPage,
  },

  {
    path: "/laboratoryarea/eforms/:id/bookingformofsmta2",
    name: "laboratoryarea-eform-bookingformofsmta2",
    component: BookingFormOfSMTA2DataPage,
  },

  {
    path: "*",
    component: AuthView,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
