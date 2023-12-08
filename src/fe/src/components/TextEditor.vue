<template>
  <v-container class="px-0">
    <v-row>
      <v-col cols="12">
        <v-label>{{ label }}</v-label>
      </v-col>
    </v-row>
    <v-row v-if="IsEditable">
      <v-col class="text-editor-menu" cols="12">
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('bold') }"
          @click="editor.chain().focus().toggleBold().run()"
        >
          <v-icon>mdi-format-bold</v-icon>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('italic') }"
          @click="editor.chain().focus().toggleItalic().run()"
        >
          <v-icon>mdi-format-italic</v-icon>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('strike') }"
          @click="editor.chain().focus().toggleStrike().run()"
        >
          <v-icon>mdi-format-strikethrough-variant</v-icon>
        </button>
        <button
          type="button"
          @click="editor.chain().focus().unsetAllMarks().run()"
        >
          <v-icon>mdi-format-clear</v-icon>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('heading', { level: 1 }) }"
          @click="editor.chain().focus().toggleHeading({ level: 1 }).run()"
        >
          <b> H1 </b>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('heading', { level: 2 }) }"
          @click="editor.chain().focus().toggleHeading({ level: 2 }).run()"
        >
          <b> H2 </b>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('heading', { level: 3 }) }"
          @click="editor.chain().focus().toggleHeading({ level: 3 }).run()"
        >
          <b> H3 </b>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('link') }"
          @click="setLink"
        >
          <v-icon>mdi-link-plus</v-icon>
        </button>
        <button
          type="button"
          @click="
            editor.chain().focus().extendMarkRange('link').unsetLink().run()
          "
        >
          <v-icon :class="{ 'is-disabled': !editor.isActive('link') }"
            >mdi-link-off</v-icon
          >
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('bulletList') }"
          @click="editor.chain().focus().toggleBulletList().run()"
        >
          <v-icon>mdi-format-list-bulleted</v-icon>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('orderedList') }"
          @click="editor.chain().focus().toggleOrderedList().run()"
        >
          <v-icon>mdi-format-list-numbered</v-icon>
        </button>
        <button
          type="button"
          :class="{ 'is-active': editor.isActive('blockquote') }"
          @click="editor.chain().focus().toggleBlockquote().run()"
        >
          <v-icon>mdi-format-quote-open</v-icon>
        </button>
        <button
          type="button"
          @click="editor.chain().focus().setHorizontalRule().run()"
        >
          <v-icon>mdi-format-align-middle</v-icon>
        </button>
        <button
          type="button"
          @click="editor.chain().focus().setHardBreak().run()"
        >
          <v-icon>mdi-format-text-wrapping-wrap</v-icon>
        </button>
        <button type="button" @click="editor.chain().focus().undo().run()">
          <v-icon>mdi-undo</v-icon>
        </button>
        <button type="button" @click="editor.chain().focus().redo().run()">
          <v-icon>mdi-redo</v-icon>
        </button>
      </v-col>
    </v-row>
    <v-row class="mt-0">
      <v-col cols="12">
        <editor-content :editor="editor" />
      </v-col>
    </v-row>
    <v-row v-if="isError">
      <v-col
        v-for="propertyError in propertyErrors"
        :key="propertyError"
        cols="12"
      >
        <v-label class="error-message" v-bind="propertyError"></v-label>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import {
  Vue,
  Component,
  Prop,
  Model,
  Provide,
  Watch,
} from "vue-property-decorator";
import StarterKit from "@tiptap/starter-kit";
import Link from "@tiptap/extension-link";
import { Editor, EditorContent } from "@tiptap/vue-2";

@Component({
  components: {
    EditorContent,
  },
})
export default class TextEditor extends Vue {
  @Model("input", { type: String }) Model!: string;

  @Prop({ type: String, default: "" }) readonly model!: string;
  @Prop({ type: String, default: "" }) readonly propertyName!: string;
  @Prop({ type: Boolean, default: false }) readonly readonly!: boolean;
  @Prop({ type: String, default: "" }) readonly label!: string;
  @Prop({ type: undefined, default: undefined })
  readonly propertiesErrors!: Map<string, Array<string>> | undefined;

  @Provide() editableEditor: Editor = new Editor({
    content: this.Model,
    extensions: [
      StarterKit,
      Link.configure({
        openOnClick: false,
        autolink: true,
      }),
    ],
    editable: true,
    onUpdate: () => {
      this.$emit("input", this.editableEditor.getHTML());
    },
  });

  @Provide() readOnlyEditor: Editor = new Editor({
    content: this.Model,
    extensions: [
      StarterKit,
      Link.configure({
        openOnClick: false,
        autolink: true,
      }),
    ],
    editable: false,
    onUpdate: () => {
      this.$emit("input", this.readOnlyEditor.getHTML());
    },
  });

  get IsEditable(): boolean {
    return this.readonly == false;
  }

  get editor(): Editor {
    if (this.IsEditable == true) {
      return this.editableEditor;
    } else {
      return this.readOnlyEditor;
    }
  }

  mounted() {
    this.editor.commands.setContent(this.Model, false);
  }

  @Watch("Model")
  onModelChanged(val: string, oldVal: string) {
    const isSame = this.editor.getHTML() === val;
    if (isSame) {
      return;
    }
    this.editor.commands.setContent(val, false);
  }

  get propertyErrors(): Array<string> {
    if (
      this.propertiesErrors === undefined ||
      this.propertiesErrors.size === 0 ||
      this.propertiesErrors.get(this.propertyName) === undefined
    ) {
      return [];
    }

    const errors = this.propertiesErrors.get(this.propertyName);
    if (errors === undefined) {
      return [];
    }

    console.log(errors);
    return errors;
  }

  get isError(): boolean {
    const errors = this.propertiesErrors?.get(this.propertyName);
    if (errors != undefined) {
      return true;
    }
    return false;
  }

  beforeDestroy() {
    this.editor.destroy();
  }

  setLink() {
    const previousUrl = this.editor.getAttributes("link").href;
    const url = window.prompt("URL", previousUrl);

    if (url === null) {
      return;
    }

    if (url === "") {
      this.editor.chain().focus().extendMarkRange("link").unsetLink().run();
      return;
    }

    this.editor
      .chain()
      .focus()
      .extendMarkRange("link")
      .setLink({ href: url })
      .run();
  }
}
</script>
<style lang="scss">
.error-message {
  color: red;
}
.text-editor-menu button {
  padding: 5px;
  margin: 2px;
  border-radius: 50%;
  height: 2.5rem;
  width: 2.5rem;
}
.text-editor-menu button.is-active {
  background-color: lightgray;
}
.text-editor-menu button .is-disabled {
  color: lightgray;
}
.text-editor-menu button:hover {
  background-color: rgb(237, 237, 237);
}
.ProseMirror {
  min-height: 450px;
  max-height: 450px;
  padding: 10px;
  overflow-y: scroll;
  border-bottom: 1px solid grey;
}
.ProseMirror:focus {
  outline: none;
}
.ProseMirror p {
  margin: 0px;
}
.ProseMirror hr {
  border: none;
  border-top: 2px solid rgba(#0d0d0d, 0.1);
  margin: 1rem 0;
}
.ProseMirror blockquote {
  padding-left: 1rem;
  border-left: 2px solid rgba(#0d0d0d, 0.1);
}
</style>
