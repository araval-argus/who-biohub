<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="materialGridItems"
      @click:row="selected"
    ></v-data-table>
    <div>
      <a
        class="v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default primary"
        href="/#/public/bmeppcatalogue"
        >See the whole catalogue</a
      >
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { PublicBmeppCatalogueGridItem } from "@/models/PublicBmeppCatalogueGridItem";
import { MaterialModule } from "../store";
import { AppModule } from "../../../store/MainStore";
import { CountryModule } from "../../countries/store";
import { LaboratoryModule } from "../../laboratories/store";

@Component({ components: {} })
export default class MaterialsTable extends Vue {
  private headers = [
    {
      text: "BMEPP Name",
      align: "start",
      sortable: true,
      value: "Name",
      width: "20px",
      fixed: true,
    },
    {
      text: "BMEPP Reference Number",
      align: "start",
      sortable: true,
      value: "ReferenceNumber",
    },
    {
      text: "Lineage",
      align: "start",
      sortable: true,
      value: "Lineage",
    },
    {
      text: "Variant",
      align: "start",
      sortable: true,
      value: "Variant",
    },
    {
      text: "Provider",
      align: "start",
      sortable: true,
      value: "ProviderLaboratory",
    },
    {
      text: "Country",
      align: "start",
      sortable: true,
      value: "Country",
    },
    {
      text: "Shared Date",
      align: "start",
      sortable: true,
      value: "BioHubFacilityDeliveryDateString",
    },
  ];

  get materialGridItems(): Array<PublicBmeppCatalogueGridItem> {
    var materials = MaterialModule.MaterialsPublic;

    if (!materials) return new Array<PublicBmeppCatalogueGridItem>();

    if (materials.length > 10) {
      materials = materials.slice(0, 10);
    }

    return materials.map((m) => {
      var laboratoryInfo = LaboratoryModule.LaboratoriesPublic.filter((l) => {
        return l.Id == m.ProviderLaboratoryId;
      }).map((m) => {
        var laboratoryCountryNameInfo = CountryModule.Countries.filter(
          (country) => {
            return country.Id == m.CountryId;
          }
        ).map((l) => {
          return {
            name: l.Name,
          };
        });

        return {
          laboratory: m.Name,
          country: laboratoryCountryNameInfo[0].name,
        };
      });

      if (laboratoryInfo.length == 0) {
        laboratoryInfo.push({ laboratory: "", country: "" });
      }

      return {
        Id: m.Id,
        ReferenceNumber: m.ReferenceNumber,
        Name: m.Name,
        Lineage: m.Lineage,
        Variant: m.Variant,
        ProviderLaboratoryId: m.ProviderLaboratoryId,
        ProviderLaboratory: laboratoryInfo[0].laboratory,
        Url: "/public/" + m.Id + "/bmeppcatalogue/detail",
        Country: laboratoryInfo[0].country,
        BioHubFacilityDeliveryDateString: this.getFormatDate(
          m.BioHubFacilityDeliveryDate
        ),
      };
    });

    // return materials.map((l) => {
    //   return {
    //     Id: l.Id,
    //     ReferenceNumber: l.ReferenceNumber,
    //     Name: l.Name,
    //     Url: "/public/" + l.Id + "/bmeppcatalogue/detail",
    //   };
    // });
  }

  getFormatDate(date: Date | string): string {
    if (date == null || date == undefined || date == "") {
      return "";
    }
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  selected(item: PublicBmeppCatalogueGridItem): void {
    var customParams = { id: item.Id };
    this.$router.push({
      name: "public-material-details",
      params: customParams,
    });
  }

  async mounted() {
    try {
      await MaterialModule.ListMaterialsPublic();
      await CountryModule.ListCountriesPublic();
      await LaboratoryModule.ListLaboratoriesPublic();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
<style>
.v-list {
  height: 900px;
  overflow-y: auto;
}

.subtitle {
  color: #c7c4c4;
}
</style>
