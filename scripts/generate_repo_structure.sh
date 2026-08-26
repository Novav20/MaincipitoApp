#!/usr/bin/env bash
# generate_repo_structure.sh - Genera el árbol de archivos limpio de MaincipitoApp ignorando bin/obj/.git

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
REPO_NAME="$(basename "$REPO_ROOT")"
OUTPUT_PATH="$REPO_ROOT/${REPO_NAME}-structure.md"

should_skip_entry() {
    local name="$1"
    case "$name" in
        .git|.vs|.vscode|.idea|bin|obj|.DS_Store|__pycache__|node_modules|*.suo|*.user)
            return 0
            ;;
        *)
            return 1
            ;;
    esac
}

emit_tree() {
    local dir_path="$1"
    local prefix="$2"

    local entries=()
    local entry
    while IFS= read -r -d '' entry; do
        entries+=("$entry")
    done < <(find "$dir_path" -mindepth 1 -maxdepth 1 -print0 | LC_ALL=C sort -z)

    local filtered=()
    for entry in "${entries[@]}"; do
        local name="$(basename "$entry")"
        if should_skip_entry "$name"; then
            continue
        fi
        filtered+=("$entry")
    done

    local count="${#filtered[@]}"
    local i
    for ((i = 0; i < count; i++)); do
        local current_path="${filtered[$i]}"
        local current_name="$(basename "$current_path")"
        local branch_prefix="${prefix}├── "
        local child_prefix="${prefix}│   "

        if [[ $i -eq $((count - 1)) ]]; then
            branch_prefix="${prefix}└── "
            child_prefix="${prefix}    "
        fi

        if [[ -d "$current_path" ]]; then
            printf '%s%s/\n' "$branch_prefix" "$current_name"
            emit_tree "$current_path" "$child_prefix"
        else
            printf '%s%s\n' "$branch_prefix" "$current_name"
        fi
    done
}

echo "Generando estructura para: $REPO_NAME..."

{
    echo "# ${REPO_NAME} Structure"
    echo ""
    echo "Repository: ${REPO_ROOT}"
    echo "Generated: $(date -Iseconds)"
    echo ""
    echo '```text'
    echo "${REPO_NAME}/"
    emit_tree "$REPO_ROOT" ""
    echo '```'
} > "$OUTPUT_PATH"

echo "Estructura exportada exitosamente a: $OUTPUT_PATH"