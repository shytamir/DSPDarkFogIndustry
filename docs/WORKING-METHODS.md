# Working methods

For status, authorization, and acceptance, see [PROJECT.md](PROJECT.md).

## Document authority

| Document | Owns | Does not own |
| --- | --- | --- |
| Owner's current instructions | Scope, priorities, authorization, acceptance | Facts unsupported by evidence |
| [AGENTS.md](../AGENTS.md) | Agent conduct and operating boundaries | Product design or work status |
| [PROJECT.md](PROJECT.md) | Steering, decisions/dispositions, all work state and acceptance | Detailed implementation or duplicated plans |
| `concept/` | Supplied product intent, dated evidence and limitations | Work authorization or current runtime certification |
| `planning/ROADMAP.md` and work items | Outcomes, order, scope, exclusions, gates, definition of done | Progress logs, acceptance claims, implementation details |
| Source and nearby comments | Algorithms, constants, integration details and maintenance constraints | Work status or approval |
| `implementation/` | Source navigation, evidence, rationale and validation procedures | Duplicated code specifications, priority or approval |
| `archive/` | Frozen historical work definitions and evidence | Active obligations |
| Build/package documentation | Reproducible operational procedures | Current release state |

When sources conflict, surface the exact conflict. Current owner steering takes
precedence; do not silently rewrite historical evidence or promote an example
into authority. Keep each fact in its designated place and link to it elsewhere.

## Bounded planning and execution

Plan with the owner. Each story needs a useful outcome, explicit inclusions and
exclusions, dependencies, a definition of done, evidence requirements, and a stop
condition for significant unknowns. Investigate uncertain native mechanisms early
in the story that needs them. Do not create a speculative architecture or a separate
validation bureaucracy before there is an executable question.

Keep major milestones as integration/acceptance gates, not vague buckets of work.
Testing belongs with the behavior introduced. Separate design approval from
authorization to implement; record both in PROJECT. A broad product concept alone
does not activate every feature it describes.

Use ordinary transitions in PROJECT: proposed, approved for planning, approved for
implementation, active, technically validated, awaiting owner acceptance, accepted,
closed or superseded. Record only applicable transitions; tooling-only work may
close without an in-game gate. A failed check, blocked task, or incomplete result
must remain visible rather than being relabelled complete.

## Evidence and human handoff

Identify the source/build, inputs, command or observation, result, and limits.
Distinguish static inspection, compilation, automated behavior checks, package
inspection, in-game observation, owner acceptance, and public-byte verification.
Do not substitute one category for another or infer a universal claim from a sample.

When human validation becomes useful, first exhaust proportionate offline checks.
Provide one coherent artifact with version, source revision, SHA-256, prerequisites,
short reproduction steps, expected observations, and a simple way to report results.
Group related checks to avoid repeated owner setup. Use a disposable save only when
an authorized runtime procedure calls for one; do not access the owner's saves as
part of repository work. Technical readiness is not owner acceptance.

Publication requires its own authority. Verify the built artifact, the public bytes,
and the publication destination separately before recording a release as published.

## Closeout and archives

When work is accepted, closed within its authorized boundary, or superseded:

1. Record its disposition and evidence links in PROJECT.
2. Move completed work definitions and associated technical records together to
   `archive/YYYY-MM-DD-short-topic/`; preserve original facts and evidence limits.
3. Repair links and leave only remaining authorized work in the active roadmap.
   With no remaining work, restore a concise placeholder linking to PROJECT.
4. Keep live operational contracts where they are still used; archive only obsolete
   revisions. A decision still in force stays in PROJECT with supporting links.

Archive text does not reactivate work, even if it says "next" or "pending".
Historical result records are evidence; current dispositions remain in PROJECT.
Do not create new documents, backlog entries, or sessions merely to show activity.

Communicate the outcome first, then the evidence and any consequential gap. Admit
uncertainty precisely. Ask for decisions only when they change the result, and
stop once the authorized definition of done is satisfied.
